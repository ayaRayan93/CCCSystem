﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraNavBar;
using MySql.Data.MySqlClient;

namespace MainSystem
{
    class SalesSystem
    {
    }
    public partial class MainForm 
    {
  
        public static XtraTabControl tabControlSales;

        

        public static Bill_Confirm objFormBillConfirm;
        public static Customer_Report objFormCustomer;
        public static XtraTabPage MainTabPageAddCustomer;
        public static XtraTabPage MainTabPageUpdateCustomer;
        public static XtraTabPage MainTabPagePrintCustomer;

        Timer timer = new Timer();
        int EmpBranchId = 0;

        public void SalesMainForm()
        {
            try
            {
                

                tabControlSales = xtraTabControlSalesContent;

                MainTabPageAddCustomer = new XtraTabPage();
                MainTabPageUpdateCustomer = new XtraTabPage();
                MainTabPagePrintCustomer = new XtraTabPage();

                EmpBranchId = UserControl.UserBranch(dbconnection);

                SpecialOrdersFunction();

                //Calculate the time of the actual work of the delegates
                timer.Interval = 1000 * 60;
                timer.Tick += new EventHandler(GetNonRequestedSpecialOrders);
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }


        private void navBarItemCustomers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage("العملاء");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("العملاء");
                    xtraTabPage = getTabPage("العملاء");
                    bindDisplayCustomersForm(xtraTabPage);
                }
                //xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemConfirmBill_LinkClicked2(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage("تاكيد الفاتورة");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("تاكيد الفاتورة");
                    xtraTabPage = getTabPage("تاكيد الفاتورة");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplayConfirmBillForm(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnProductSellPrice_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage("البنود");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("البنود");
                    xtraTabPage = getTabPage("البنود");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplayProductsSellPriceForm(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOffers_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage("العروض");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("العروض");
                    xtraTabPage = getTabPage("العروض");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplayOffersForm(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void navBarItemSpecialOrdersReport_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            /*try
            {
                restForeColorOfNavBarItem();
                NavBarItem navBarItem = (NavBarItem)sender;
                navBarItem.Appearance.ForeColor = Color.Blue;

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage("عرض الطلبات الخاصة");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("عرض الطلبات الخاصة");
                    xtraTabPage = getTabPage("عرض الطلبات الخاصة");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplaySpecialOrdersReport(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (flag == false)
                {
                    xtraTabControlMainContainer.TabPages.Insert(1, StoreTP);
                    flag = true;
                }
                xtraTabControlMainContainer.SelectedTabPage = xtraTabControlMainContainer.TabPages[1];

                if (!xtraTabControlSalesContent.Visible)
                    xtraTabControlSalesContent.Visible = true;

                XtraTabPage xtraTabPage = getTabPage("عرض الطلبات الخاصة");
                if (xtraTabPage == null)
                {
                    xtraTabControlSalesContent.TabPages.Add("عرض الطلبات الخاصة");
                    xtraTabPage = getTabPage("عرض الطلبات الخاصة");
                }
                xtraTabPage.Controls.Clear();

                xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
                bindDisplaySpecialOrdersReport(xtraTabPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    
        //functions
        //Products sell price
        public void bindDisplayProductsSellPriceForm(XtraTabPage xtraTabPage)
        {
            ProductsSellPriceForm objForm = new ProductsSellPriceForm(this);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //record sell price 
        public void bindRecordSellPriceForm(ProductsSellPriceForm productsSellPriceForm)
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تسجيل اسعار البنود");
            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تسجيل اسعار البنود");
                xtraTabPage = getTabPage("تسجيل اسعار البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
            SetSellPrice objForm = new SetSellPrice(productsSellPriceForm, xtraTabControlSalesContent);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //update sell price 
        public void bindUpdateSellPriceForm(List<DataRowView> rows, ProductsSellPriceForm productsSellPriceForm, String query)
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تعديل اسعار البنود");

            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تعديل اسعار البنود");
                xtraTabPage = getTabPage("تعديل اسعار البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;

            UpdateSellPrice objForm = new UpdateSellPrice(rows, productsSellPriceForm, query, xtraTabControlSalesContent);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }
        //report sell price 
        public void bindReportSellPriceForm(GridControl gridControl)
        {
            if (!xtraTabControlSalesContent.Visible)
                xtraTabControlSalesContent.Visible = true;

            XtraTabPage xtraTabPage = getTabPage("تقرير اسعار البنود");

            if (xtraTabPage == null)
            {
                xtraTabControlSalesContent.TabPages.Add("تقرير اسعار البنود");
                xtraTabPage = getTabPage("تقرير اسعار البنود");
            }
            xtraTabPage.Controls.Clear();

            xtraTabControlSalesContent.SelectedTabPage = xtraTabPage;
            ProductSellPricesReport objForm = new ProductSellPricesReport(gridControl);
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        //offers
        public void bindDisplayOffersForm(XtraTabPage xtraTabPage)
        {
            //OffersSellPrice objForm = new OffersSellPrice();
            //objForm.TopLevel = false;

            //xtraTabPage.Controls.Add(objForm);
            //objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //objForm.Dock = DockStyle.Fill;
            //objForm.Show();
        }

        //customers
        public void bindDisplayCustomersForm(XtraTabPage xtraTabPage)
        {
            objFormCustomer = new Customer_Report();
            objFormCustomer.TopLevel = false;

            xtraTabPage.Controls.Add(objFormCustomer);
            objFormCustomer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objFormCustomer.Dock = DockStyle.Fill;
            objFormCustomer.Show();
        }
        //confirm bill
        public void bindDisplayConfirmBillForm(XtraTabPage xtraTabPage)
        {
            objFormBillConfirm = new Bill_Confirm();
            objFormBillConfirm.TopLevel = false;

            xtraTabPage.Controls.Add(objFormBillConfirm);
            objFormBillConfirm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objFormBillConfirm.Dock = DockStyle.Fill;
            objFormBillConfirm.Show();
        }
        //Special Orders Report
        public void bindDisplaySpecialOrdersReport(XtraTabPage xtraTabPage)
        {
            SpecialOrders_Report objForm = new SpecialOrders_Report();
            objForm.TopLevel = false;

            xtraTabPage.Controls.Add(objForm);
            objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            objForm.Dock = DockStyle.Fill;
            objForm.Show();
        }

        public XtraTabPage getTabPage(string text)
        {
            for (int i = 0; i < xtraTabControlSalesContent.TabPages.Count; i++)
                if (xtraTabControlSalesContent.TabPages[i].Text == text)
                {
                    return xtraTabControlSalesContent.TabPages[i];
                }
            return null;
        }



        //
        public void GetNonRequestedSpecialOrders(object sender, EventArgs e)
        {
            try
            {
                SpecialOrdersFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        public void SpecialOrdersFunction()
        {
            string query = "SELECT Count(special_order.SpecialOrder_ID) FROM special_order INNER JOIN dash ON special_order.Dash_ID = dash.Dash_ID INNER JOIN requests ON special_order.SpecialOrder_ID = requests.SpecialOrder_ID where special_order.Record=0 AND dash.Branch_ID=" + EmpBranchId;
            MySqlCommand command = new MySqlCommand(query, dbconnection);
            dbconnection.Open();
            string reader = command.ExecuteScalar().ToString();
            labelNotify.Text = reader;
        }
    }
}