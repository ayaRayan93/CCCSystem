﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainSystem
{
    public partial class SetFak : Form
    {
        MySqlConnection dbconnection, dbconnection1, dbconnection2;
        bool loaded = false;
        bool flag = false;
        AtaqmStorage AtaqmStorage;
        public SetFak(AtaqmStorage AtaqmStorage)
        {
            InitializeComponent();
            dbconnection = new MySqlConnection(connection.connectionString);
            dbconnection1 = new MySqlConnection(connection.connectionString);
            dbconnection2 = new MySqlConnection(connection.connectionString);
            this.AtaqmStorage = AtaqmStorage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from store ";
                MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comStore.DataSource = dt;
                comStore.DisplayMember = dt.Columns["Store_Name"].ToString();
                comStore.ValueMember = dt.Columns["Store_ID"].ToString();
                comStore.Text = "";
                txtStoreID.Text = "";

                query = "select distinct Factory_Name,sets.Factory_ID from sets inner join Factory on sets.Factory_ID=Factory.Factory_ID";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comFactory.DataSource = dt;
                comFactory.DisplayMember = dt.Columns["Factory_Name"].ToString();
                comFactory.ValueMember = dt.Columns["Factory_ID"].ToString();
                comFactory.Text = "";
                txtFactory.Text = "";

                query = "select distinct Type_Name,type.Type_ID from sets inner join type on sets.Type_ID=type.Type_ID";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comType.DataSource = dt;
                comType.DisplayMember = dt.Columns["Type_Name"].ToString();
                comType.ValueMember = dt.Columns["Type_ID"].ToString();
                comType.Text = "";
                txtType.Text = "";

                query = "select distinct Group_Name,sets.Group_ID from sets inner join groupo on sets.Group_ID=groupo.Group_ID";
                da = new MySqlDataAdapter(query, dbconnection);
                dt = new DataTable();
                da.Fill(dt);
                comGroup.DataSource = dt;
                comGroup.DisplayMember = dt.Columns["Group_Name"].ToString();
                comGroup.ValueMember = dt.Columns["Group_ID"].ToString();
                comGroup.Text = "";
                txtGroup.Text = "";

                loaded = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void comBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (loaded)
                {
                    ComboBox comBox = (ComboBox)sender;

                    switch (comBox.Name)
                    {
                        case "comFactory":
                            txtFactory.Text = comFactory.SelectedValue.ToString();
                            Search();
                            break;
                        case "comType":
                            txtType.Text = comType.SelectedValue.ToString();
                            Search();
                            break;
                        case "comGroup":
                            txtGroup.Text = comGroup.SelectedValue.ToString();
                            Search();
                            break;
                        case "comStore":
                            txtStoreID.Text = comStore.SelectedValue.ToString();
                            break;
                        case "comSets":
                            if (flag)
                            {
                                txtSetsID.Text = comSets.SelectedValue.ToString();
                                displayQuantitySet();
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string query;
            MySqlCommand com;
            string Name;
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtBox.Text != "")
                    {
                        dbconnection.Open();
                        switch (txtBox.Name)
                        {
                            case "txtFactory":
                                query = "select DISTINCT Factory_Name from sets inner join factory on sets.Factory_ID=factory.Factory_ID where sets.Factory_ID='" + txtFactory.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comFactory.Text = Name;
                                    Search();
                                    txtGroup.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtType":
                                query = "select DISTINCT Type_Name from sets  inner join type on sets.Type_ID=type.Type_ID  where sets.Type_ID='" + txtType.Text + "'";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comType.Text = Name;
                                    Search();
                                    txtFactory.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtGroup":
                                query = "select DISTINCT Group_Name from sets inner join groupo on sets.Group_ID=groupo.Group_ID  where sets.Group_ID=" + txtGroup.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comGroup.Text = Name;
                                    Search();
                                    txtSetsID.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtStoreID":
                                query = "select Store_Name from store where Store_ID=" + txtStoreID.Text + "";
                                com = new MySqlCommand(query, dbconnection);
                                if (com.ExecuteScalar() != null)
                                {
                                    Name = (string)com.ExecuteScalar();
                                    comStore.Text = Name;
                                    txtType.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("there is no item with this id");
                                    dbconnection.Close();
                                    return;
                                }
                                break;
                            case "txtSetsID":
                                if (flag)
                                {
                                    query = "select Set_Name from sets where Set_ID=" + txtSetsID.Text + "";
                                    com = new MySqlCommand(query, dbconnection);
                                    if (com.ExecuteScalar() != null)
                                    {
                                        Name = (string)com.ExecuteScalar();
                                        comSets.Text = Name;
                                        displayQuantitySet();
                                        txtStoreID.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show("there is no item with this id");
                                        dbconnection.Close();
                                        return;
                                    }
                                }
                                break;
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                dbconnection.Close();
            }
        }

        private void btnFak_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                dataGridView1.Rows.Clear();
                double quantitySet;
                if (double.TryParse(txtSetQuantity.Text, out quantitySet))
                {
                    int id;
                    if (int.TryParse(txtSetsID.Text, out id))
                    {
                        double q = fakSet(quantitySet,id);
                        increaseItemsQuantity(q, id);
                    }
                    else
                    {
                        MessageBox.Show("enter correct value");
                    }
                }
                else
                {
                    MessageBox.Show("enter correct value");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                dbconnection.Open();
                RecordSetQuantityInStorage(Convert.ToDouble(txtSetQuantity.Text), Convert.ToInt16(txtSetsID.Text));
                increaseItemsQuantityInDB(Convert.ToDouble(txtSetQuantity.Text), Convert.ToInt16(txtSetsID.Text));
                MessageBox.Show("تم");

                dataGridView1.Rows.Clear();
                AtaqmStorage.DisplayAtaqm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }

        private void btnNewChooes_Click(object sender, EventArgs e)
        {
            try
            {
                comStore.Text = "";
                comType.Text = "";
                comFactory.Text = "";
                comGroup.Text = "";
                comSets.Text = "";

                txtStoreID.Text = "";
                txtType.Text = "";
                txtFactory.Text = "";
                txtGroup.Text = "";

                txtSetsID.Text = "";
                txtSetQuantity.Text = "";

                dataGridView1.Rows.Clear();

                Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //functions 
        //display quantity of set in store
        public void displayQuantitySet()
        {
            dbconnection.Close();
            dbconnection.Open();
            int SetID, StoreID;
            if (int.TryParse(txtSetsID.Text, out SetID) && int.TryParse(txtStoreID.Text, out StoreID))
                {
                string query = "select Total_Meters from storage  where Set_ID='" + SetID + "' and Store_ID=" + StoreID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                if (com.ExecuteScalar() != null)
                {
                    double storeQuantity = Convert.ToDouble(com.ExecuteScalar());
                    txtTotalQuantitySet.Text = storeQuantity.ToString();
                }
                else
                {
                    MessageBox.Show("هذا الطقم لايوجد في المخزن");
                }
            }
            else
            {
                MessageBox.Show("ادخل كمية صحيحة");
            }
            dbconnection.Close();
        }
        //الطقم تفكيك
        public double fakSet(double quantitySetFak, int SetID)
        {
           
            string query = "select Total_Meters from storage  where Set_ID='" + SetID+ "' and Store_ID=" + txtStoreID.Text;
            MySqlCommand com = new MySqlCommand(query,dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double storeQuantity = Convert.ToDouble(com.ExecuteScalar());
                if (quantitySetFak <= storeQuantity)
                {
                   return quantitySetFak;
                }
                else
                {
                    MessageBox.Show("الكمية غير متوفرة");
                }
            }
            else
            {
                MessageBox.Show("هذا الطقم لايوجد في المخزن");
            }
            return -1;
        }
        //record to database
        public void RecordSetQuantityInStorage(double newQuantitySet, int SetID)
        {
            string query = "select Total_Meters from storage  where Set_ID='" + SetID + "' and Store_ID=" + txtStoreID.Text;
            MySqlCommand com = new MySqlCommand(query, dbconnection);
            if (com.ExecuteScalar() != null)
            {
                double storeQuantity = Convert.ToDouble(com.ExecuteScalar());
                if ((storeQuantity - newQuantitySet) > 0)
                {
                    query = "update storage set Total_Meters =" + (storeQuantity - newQuantitySet) + "  where Set_ID=" + SetID + " and Store_ID=" + txtStoreID.Text;
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                }
                else
                {
                    query = "delete from storage  where Set_ID=" + SetID + " and Store_ID=" + txtStoreID.Text;
                    com = new MySqlCommand(query, dbconnection);
                    com.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("هذا الطقم لايوجد في المخزن");
            }
        }
        //increase the quantity of taqm items
        public void increaseItemsQuantity(double TaqmQuantity,int setID)
        {
            if (TaqmQuantity>0)
            {
                dbconnection1.Open();
                dbconnection2.Open();
                string query = "select Data_ID,Quantity from set_details  where Set_ID=" + setID;
                MySqlCommand com = new MySqlCommand(query,dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while(dr.Read())
                {
                    query = "select sum(Total_Meters) from storage where Data_ID='" + dr["Data_ID"].ToString()+ "' group by Store_ID having Store_ID=" + txtStoreID.Text ;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection1);
                    double QuantityInStore = Convert.ToDouble(com1.ExecuteScalar());
                    double QuantityInSet= Convert.ToDouble(dr["Quantity"].ToString());
                    double newQuantity = QuantityInStore+(QuantityInSet * TaqmQuantity);
                    query = "SELECT data.Data_ID, data.Code as 'الكود',type.Type_Name as 'النوع',factory.Factory_Name as 'المصنع',groupo.Group_Name as 'المجموعة',product.Product_Name as 'الصنف',sort.Sort_Value as 'الفرز',color.Color_Name as 'اللون',size.Size_Value as 'المقاس',data.Classification as 'التصنيف',data.Description as 'الوصف',data.Carton as 'الكرتنة' from data INNER JOIN type ON type.Type_ID = data.Type_ID INNER JOIN product ON product.Product_ID = data.Product_ID INNER JOIN factory ON data.Factory_ID = factory.Factory_ID INNER JOIN groupo ON data.Group_ID = groupo.Group_ID LEFT outer JOIN color ON data.Color_ID = color.Color_ID LEFT outer  JOIN size ON data.Size_ID = size.Size_ID LEFT outer  JOIN sort ON data.Sort_ID = sort.Sort_ID  where Data_ID=" + dr["Data_ID"].ToString() + "";

                    com = new MySqlCommand(query, dbconnection1);
                    MySqlDataReader dataReader1 = com.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells["ProductCode"].Value = dataReader1["الكود"].ToString();
                        dataGridView1.Rows[n].Cells["Type_Name"].Value = dataReader1["النوع"].ToString();
                        dataGridView1.Rows[n].Cells["Factory_Name"].Value = dataReader1["المصنع"].ToString();
                        dataGridView1.Rows[n].Cells["Group_Name"].Value = dataReader1["المجموعة"].ToString();
                        dataGridView1.Rows[n].Cells["ProductName"].Value = dataReader1["الصنف"].ToString();
                        dataGridView1.Rows[n].Cells["ProductColor"].Value = dataReader1["اللون"].ToString();
                        dataGridView1.Rows[n].Cells["productSize"].Value = dataReader1["المقاس"].ToString();
                        dataGridView1.Rows[n].Cells["ProductSort"].Value = dataReader1["الفرز"].ToString();
                        dataGridView1.Rows[n].Cells["ProductQuantity"].Value = newQuantity.ToString();
                    }
                    dataReader1.Close();
                }
                dr.Close();
               
            }
            dbconnection1.Close();
            dbconnection2.Close();
        }        
        //
        public void increaseItemsQuantityInDB(double TaqmQuantity, int setID)
        {
            if (TaqmQuantity > 0)
            {
                dbconnection1.Open();
                dbconnection2.Open();
                string query = "select Data_ID,Quantity from set_details  where Set_ID=" + setID;
                MySqlCommand com = new MySqlCommand(query, dbconnection);
                MySqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    query = "select sum(Total_Meters) from storage where Data_ID='" + dr["Data_ID"].ToString() + "' group by Store_ID having Store_ID=" + txtStoreID.Text;
                    MySqlCommand com1 = new MySqlCommand(query, dbconnection1);
                    double QuantityInStore = Convert.ToDouble(com1.ExecuteScalar());
                    double QuantityInSet = Convert.ToDouble(dr["Quantity"].ToString());
                    double newQuantity = QuantityInSet * TaqmQuantity;

                    query = "select Storage_ID,Total_Meters from storage where Data_ID='" + dr["Data_ID"].ToString() + "' and Store_ID=" + txtStoreID.Text;
                    com1 = new MySqlCommand(query, dbconnection1);
                    MySqlDataReader dr2 = com1.ExecuteReader();
                    int id = 0;
                    while (dr2.Read())
                    {
                        double storageQ = Convert.ToDouble(dr2["Total_Meters"]);
                        //if (storageQ > newQuantity)
                        //{
                            id = Convert.ToInt16(dr2["Storage_ID"]);
                            query = "update storage set Total_Meters=" + (storageQ + newQuantity) + " where Storage_ID=" + id;
                            MySqlCommand comm = new MySqlCommand(query, dbconnection2);
                            comm.ExecuteNonQuery();
                            break;
                        //}
                    
                    }
                    dr2.Close();
                }
                dr.Close();
              
            }
            dbconnection1.Close();
            dbconnection2.Close();
        }
        //
        private void Search()
        {
            try
            {
                string q1;
                if (txtType.Text != "" && txtFactory.Text != "" && txtGroup.Text != "")
                {
                    q1 = "Type_ID =" + txtType.Text + " and Factory_ID=" + txtFactory.Text + " and Group_ID=" + txtGroup.Text;

                    string query = "select Set_ID,Set_Name from sets where " + q1;
                    MySqlDataAdapter da = new MySqlDataAdapter(query, dbconnection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comSets.DataSource = dt;
                    comSets.DisplayMember = dt.Columns["Set_Name"].ToString();
                    comSets.ValueMember = dt.Columns["Set_ID"].ToString();
                    groupBox1.Visible = true;
                    comSets.Text = "";
                    txtSetsID.Text = "";
                    flag = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dbconnection.Close();
        }
 
    }
}
