﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_System.BL
{
    public partial class FRM_Allpayedmoney : Form
    {
        BL.CLS_allPayedmoney cls = new BL.CLS_allPayedmoney();
        DataTable dt;
        bool chare = true;
        public FRM_Allpayedmoney()
        {
            InitializeComponent();
            load_all_payedmoneyName();
            radioButton1.Checked = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void load_all_payedmoney()
        {
            DataTable ds = new DataTable();
            ds = cls.LOAD_ALL_Payedmoney_todgv();
            dataGridView1.DataSource = ds;
            lb_NUM_CUSTOMER.Text = "عددالمصروفات:" + " " + dataGridView1.RowCount.ToString();
        }
        public void load_all_payedmoneyName()
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            dt = new DataTable();
            dt = DAL.SelectData("load_payedmoney_NAME", null);
            for (int i = 0; i < dt.Rows.Count; i++)
                cmbCUT_Name.Items.Add(dt.Rows[i].ItemArray[0]);
        }

        private void FRM_Allpayedmoney_Load(object sender, EventArgs e)
        {
            load_all_payedmoney();
        }

        private void btn_search_cut_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            if(cmbCUT_Name.Text!="")
            {
            DataTable dt = new DataTable();
            dt = cls.SEARCH_AllPayedmoney_NAME(cmbCUT_Name.SelectedItem.ToString());
            dataGridView1.DataSource = dt;
            lb_NUM_CUSTOMER.Text = "عددالمصروفات:" + " " + dataGridView1.RowCount.ToString();
            }
            else
            {
                load_all_payedmoney();
            }
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("هل انت متاكد من تعديل معلومات المصروف؟", "عملية التعديل", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                PL.FRM_paymoney frm = new PL.FRM_paymoney();
                frm.tbPayID.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frm.tb_PayName.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frm.tb_payDescription.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                frm.tb_Payamount.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                frm.dateTimePicker1.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
                frm.Text = "تعديل معلومات المصروف " + this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                frm.btn_ADD_payedmoney.Text = "تعديل";
                frm.state = "update";
                frm.ShowDialog();  
                load_all_payedmoney();

            }
            else
            {
                MessageBox.Show("تم الغاء عملية التعديل", "عملية التعديل", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
       }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من حذف المصروف؟", "عملية الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                cls.DELETE_Payedmoney(Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[0].Value));
                load_all_payedmoney();
                MessageBox.Show("تم الحذف بنجاح", "عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("تم الغاء عملية الحذف", "عملية الحذف", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btn_ADD_payedmoney_Click(object sender, EventArgs e)
        {
            PL.FRM_paymoney frm = new PL.FRM_paymoney();
            frm.ShowDialog();
            load_all_payedmoney();
            cmbCUT_Name.Items.Clear();
            load_all_payedmoneyName();

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false)
            {
                flag(true);
            }
            if (radioButton1.Checked == true)
            {
                flag(false);
                load_all_payedmoney();
            }

        }
        public void flag(bool _chare)
        {
            chare = _chare;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && chare == true)
            {
                radioButton1.Checked = false;
            }
            chare = true;
            cmbCUT_Name.SelectedIndex = 0;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();       
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            this.تعديلToolStripMenuItem_Click(sender, e);
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            this.حذفToolStripMenuItem_Click(sender, e);
        }



    }
}
