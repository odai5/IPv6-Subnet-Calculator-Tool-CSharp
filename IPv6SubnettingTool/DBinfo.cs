﻿/*
 * Copyright (c) 2010-2019 Yucel Guven
 * All rights reserved.
 * 
 * This file is part of IPv6 Subnetting Tool.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted (subject to the limitations in the
 * disclaimer below) provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 * notice, this list of conditions and the following disclaimer in the
 * documentation and/or other materials provided with the distribution.
 * 
 * NO EXPRESS OR IMPLIED LICENSES TO ANY PARTY'S PATENT RIGHTS ARE
 * GRANTED BY THIS LICENSE. THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS 
 * AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, 
 * BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS 
 * FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER
 * OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY,
 * OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Globalization;

namespace IPv6SubnettingTool
{
    public partial class DBinfo : Form
    {
        #region special variables - yucel
        public DBServerInfo ServerInfo = new DBServerInfo();
        public CultureInfo culture;
        public delegate void ChangeWinFormStringsDelegate(CultureInfo culture);
        public event ChangeWinFormStringsDelegate ChangeUILanguage = delegate { };
        #endregion

        public DBinfo(CultureInfo culture)
        {
            InitializeComponent();

            this.culture = culture;
            this.SwitchLanguage(this.culture);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPHostEntry hostent = null;
                IPAddress ipaddr = null;

                if (!IPAddress.TryParse(this.textBox1.Text.Trim(), out ipaddr))
                {
                    hostent = Dns.GetHostEntry(this.textBox1.Text.Trim());
                    this.ServerInfo.ServerIP = hostent.AddressList[0];
                }
                else
                    this.ServerInfo.ServerIP = ipaddr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox1.BackColor = Color.Yellow;
                return;
            }

            if (! UInt16.TryParse(this.textBox7.Text.Trim(), out this.ServerInfo.PortNum))
            {
                this.textBox7.BackColor = Color.Yellow;
                return;
            }

            this.ServerInfo.DBname = this.textBox2.Text.Trim();
            this.ServerInfo.Username = this.textBox3.Text.Trim();
            this.ServerInfo.Password = this.textBox4.Text.Trim();
            this.ServerInfo.Tablename = this.textBox5.Text.Trim();

            if (this.ServerInfo.DBname == "")
            {
                this.textBox2.BackColor = Color.Yellow;
                return;
            }
            if (this.ServerInfo.Tablename == "")
            {
                this.textBox5.BackColor = Color.Yellow;
                return;
            }
            if (this.ServerInfo.Username == "")
            {
                this.textBox3.BackColor = Color.Yellow;
                return;
            }
            if (this.ServerInfo.Password == "")
            {
                this.textBox4.BackColor = Color.Yellow;
                return;
            }
            
            if (this is IDisposable)
                this.Dispose();
            else
                this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ServerInfo.Trytoconnect = false;

            if (this is IDisposable)
                this.Dispose();
            else
                this.Close();
        }

        private void DBinfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.ServerInfo.Trytoconnect = false;

                if (this is IDisposable)
                    this.Dispose();
                else
                    this.Close();
            }
        }

        public void SwitchLanguage(CultureInfo culture)
        {
            this.culture = culture;
            this.Text = StringsDictionary.KeyValue("DBinfo_Form.Text", this.culture);
            this.button1.Text = StringsDictionary.KeyValue("DBinfo_button1.Text", this.culture);
            this.button2.Text = StringsDictionary.KeyValue("DBinfo_button2.Text", this.culture);
            this.label1.Text = StringsDictionary.KeyValue("DBinfo_label1.Text", this.culture);
            this.label2.Text = StringsDictionary.KeyValue("DBinfo_label2.Text", this.culture);
            this.label3.Text = StringsDictionary.KeyValue("DBinfo_label3.Text", this.culture);
            this.label4.Text = StringsDictionary.KeyValue("DBinfo_label4.Text", this.culture);
            this.label5.Text = StringsDictionary.KeyValue("DBinfo_label5.Text", this.culture);
            this.label6.Text = StringsDictionary.KeyValue("DBinfo_label6.Text", this.culture);
            this.label7.Text = StringsDictionary.KeyValue("DBinfo_label7.Text", this.culture);
            this.textBox6.Text = StringsDictionary.KeyValue("DBinfo_textBox6.Text", this.culture);
            this.ChangeUILanguage.Invoke(this.culture);
        }

        private void DBinfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.ServerInfo.Trytoconnect = false;

                if (this is IDisposable)
                    this.Dispose();
                else
                    this.Close();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.textBox1.BackColor = Color.White;
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            this.textBox7.BackColor = Color.White;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            this.textBox2.BackColor = Color.White;
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            this.textBox5.BackColor = Color.White;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            this.textBox3.BackColor = Color.White;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            this.textBox4.BackColor = Color.White;
        }

        private void DBinfo_Load(object sender, EventArgs e)
        {
            if (this.ServerInfo.ServerIP != null)
                this.textBox1.Text = this.ServerInfo.ServerIP.ToString();
            this.textBox7.Text = this.ServerInfo.PortNum.ToString();
            this.textBox2.Text = this.ServerInfo.DBname;
            this.textBox5.Text = this.ServerInfo.Tablename;
            this.textBox3.Text = this.ServerInfo.Username;
        }
    }
}
