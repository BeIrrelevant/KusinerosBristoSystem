using Dbsys.AppData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dbsys.Forms
{
    public partial class FrmCustomerPOS : Form
    {
        public List<AppData.Menu> listMenu;

        double Total = 0;

        private int margin = 90;
        private int lineSpacing = 40;
        private int y = 370;
        private int a = 550;
        private int b = 650;

        string currencySymbol = "₱";

        public FrmCustomerPOS()
        {
            InitializeComponent();
            listMenu = new List<AppData.Menu>();
        }

        //private void FrmPOS_Load(object sender, EventArgs e)
        //{
        //    lblUserId.Text = UserLogged.GetInstance().UserAccount.userId.ToString();
        //    lblUsername.Text = UserLogged.GetInstance().UserAccount.userName.ToString();

        //    int userId = int.Parse(lblUserId.Text);

        //    SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
        //    con.Open();


        //    // Load data into dataGridViewOrderDetails
        //    SqlCommand cmdOrderDetails = new SqlCommand("SELECT * FROM OrderDetails WHERE userId = '" + userId + "'", con);
        //    SqlDataAdapter daOrderDetails = new SqlDataAdapter(cmdOrderDetails);
        //    DataTable dtOrderDetails = new DataTable();
        //    daOrderDetails.Fill(dtOrderDetails);
        //    GetMax.GetData.GetMaxOrderNumber();
        //    txtOrderNumber.Text = GetMax.GlobalDeclaration.OrderNumber.ToString();
        //    // dataGridViewOrderDetails.DataSource = dtOrderDetails;);



        //    SqlCommand cmdMenu = new SqlCommand("SELECT * FROM Menu", con);
        //    SqlDataAdapter daMenu = new SqlDataAdapter(cmdMenu);
        //    DataTable dtMenu = new DataTable();
        //    daMenu.Fill(dtMenu);
        //    GetMax.GetData.GetMaxItem();
        //    dataGridViewPRICES.DataSource = dtMenu;

        //    SqlCommand cmdPurchase = new SqlCommand("SELECT * FROM Purchase WHERE userId = '" + userId + "' AND CustomerName = '" + txtCustomerName.Text + "'", con);
        //    SqlDataAdapter daPurchase = new SqlDataAdapter(cmdPurchase);
        //    DataTable dtPurchase = new DataTable();
        //    daPurchase.Fill(dtPurchase);
        //    GetMax.GetData.GetNumItem();
        //    lblItemNo.Text = GetMax.GlobalDeclaration.ItemNo.ToString();
        //    dataGridViewPurchase.DataSource = dtPurchase;

        //    //  loadCbBox();

        //}
        private void clearfields()
        {
            txtCustomerName.Clear();
            txtContactNumber.Clear();
            txtAddress.Clear();
            txtTotalBill.Clear();
            txtAmount.Clear();
            txtChange.Clear();
            cbBoxDiningOption.SelectedIndex = 0;
            txtQuantity.Clear();
            txtAddTotal.Clear();
            txtPriceofMenu.Clear();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
            con.Open();

            SqlCommand cmdOrderDetails = new SqlCommand("INSERT INTO OrderDetails (OrderNumber, userName, CustomerName, ContactNumber, Address, TotalBill, CashReceived, Discount, Change, DiningOption, userId) " +
                "VALUES (@OrderNumber, @userName, @CustomerName, @ContactNumber, @Address, @TotalBill, @CashReceived, @Discount, @Change, @DiningOption, @userId)", con);

            cmdOrderDetails.Parameters.AddWithValue("@OrderNumber", Convert.ToInt32(txtOrderNumber.Text));
            cmdOrderDetails.Parameters.AddWithValue("@userName", lblUsername.Text);
            cmdOrderDetails.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
            cmdOrderDetails.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Text);
            cmdOrderDetails.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmdOrderDetails.Parameters.AddWithValue("@TotalBill", Convert.ToDecimal(txtTotalBill.Text));
            cmdOrderDetails.Parameters.AddWithValue("@CashReceived", Convert.ToDecimal(txtAmount.Text));
            cmdOrderDetails.Parameters.AddWithValue("@Discount", cbDiscount.Text);
            cmdOrderDetails.Parameters.AddWithValue("@Change", Convert.ToDecimal(txtAmount.Text));
            cmdOrderDetails.Parameters.AddWithValue("@DiningOption", cbBoxDiningOption.Text);
            cmdOrderDetails.Parameters.AddWithValue("@userId", lblUserId.Text);

            cmdOrderDetails.ExecuteNonQuery();

            con.Close();

            FrmCustomerPOS_Load(sender, e);
            clearfields();
            MessageBox.Show("Successfully Inserted!!");

        }

        private void FrmCustomerPOS_Load(object sender, EventArgs e)
        {
            lblUserId.Text = UserLogged.GetInstance().UserAccount.userId.ToString();
            lblUsername.Text = UserLogged.GetInstance().UserAccount.userName.ToString();

            int userId = int.Parse(lblUserId.Text);

            SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True");
            con.Open();

            // Load data into dataGridViewOrderDetails
            SqlCommand cmdOrderDetails = new SqlCommand("SELECT * FROM OrderDetails WHERE userId = '" + userId + "'", con);
            SqlDataAdapter daOrderDetails = new SqlDataAdapter(cmdOrderDetails);
            DataTable dtOrderDetails = new DataTable();
            daOrderDetails.Fill(dtOrderDetails);

            // Get the maximum order number
            GetMax.GetData.GetMaxOrderNumber();

            // Assign the maximum order number to txtOrderNumber
            txtOrderNumber.Text = GetMax.GlobalDeclaration.OrderNumber.ToString();



            SqlCommand cmdPurchase = new SqlCommand("SELECT * FROM Purchase WHERE userId = '" + userId + "'", con);
            SqlDataAdapter daPurchase = new SqlDataAdapter(cmdPurchase);
            DataTable dtPurchase = new DataTable();
            daPurchase.Fill(dtPurchase);
            GetMax.GetData.GetNumItem();
            lblItemNo.Text = GetMax.GlobalDeclaration.ItemNo.ToString();
            dataGridViewPurchase.DataSource = dtPurchase;



            loadCbBox();

        }
        private void loadCbBox()
        {
            using (var db = new DBSYSEntities())
            {
                listMenu = db.Menu.ToList();

                // Filter menus by category and assign them to the respective combo boxes
                cbDishes.DisplayMember = "MenuName";
                cbDishes.ValueMember = "MenuId";
                cbDishes.DataSource = listMenu.Where(m => m.MenuCategory == "Dishes").ToList();
                cbDishes.DropDownStyle = ComboBoxStyle.DropDownList; // Set DropDownStyle
                cbDishes.SelectedIndex = -1; // Set selected index to -1

                cbDrinks.DisplayMember = "MenuName";
                cbDrinks.ValueMember = "MenuId";
                cbDrinks.DataSource = listMenu.Where(m => m.MenuCategory == "Drinks").ToList();
                cbDrinks.DropDownStyle = ComboBoxStyle.DropDownList; // Set DropDownStyle
                cbDrinks.SelectedIndex = -1; // Set selected index to -1

                cbDessert.DisplayMember = "MenuName";
                cbDessert.ValueMember = "MenuId";
                cbDessert.DataSource = listMenu.Where(m => m.MenuCategory == "Dessert").ToList();
                cbDessert.DropDownStyle = ComboBoxStyle.DropDownList; // Set DropDownStyle
                cbDessert.SelectedIndex = -1; // Set selected index to -1
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmCustomerPOS_Load(sender, e);
            clearfields();
        }

        private void cbDishes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedId = Convert.ToInt32(cbDishes.SelectedValue);

                AppData.Menu selectedDish = listMenu.FirstOrDefault(c => c.MenuId == selectedId);

                if (selectedDish != null)
                {
                    pictureBox2.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\MenuImage\\" + selectedDish.MenuImg);
                    txtPriceofMenu.Text = currencySymbol + selectedDish.MenuPrice.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedId = Convert.ToInt32(cbDrinks.SelectedValue);

                AppData.Menu selectedDrink = listMenu.FirstOrDefault(c => c.MenuId == selectedId);

                if (selectedDrink != null)
                {

                    pictureBox2.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\MenuImage\\" + selectedDrink.MenuImg);
                    txtPriceofMenu.Text = currencySymbol + selectedDrink.MenuPrice.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbDessert_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedId = Convert.ToInt32(cbDessert.SelectedValue);

                AppData.Menu selectedDessert = listMenu.FirstOrDefault(c => c.MenuId == selectedId);

                if (selectedDessert != null)
                {
                    pictureBox2.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + "\\MenuImage\\" + selectedDessert.MenuImg);
                    txtPriceofMenu.Text = currencySymbol + selectedDessert.MenuPrice.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPurchase.Rows[e.RowIndex];

                lblItemNo.Text = row.Cells["ItemNo"].Value.ToString();
                string selectedOrder = row.Cells["Orders"].Value.ToString();
                cbDishes.SelectedItem = selectedOrder;
                cbDrinks.SelectedItem = selectedOrder;
                cbDessert.SelectedItem = selectedOrder;
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtPriceofMenu.Text = row.Cells["Price"].Value.ToString();
                txtAddTotal.Text = row.Cells["Total"].Value.ToString();
                txtCustomerName.Text = row.Cells["CustomerName"].Value.ToString();
                lblUserId.Text = row.Cells["userId"].Value.ToString();


            }
        }

        private void btnAddTotalOrders_Click(object sender, EventArgs e)
        {
            double itemPrice = 0;
            double quantity = Convert.ToInt32(txtQuantity.Text);

            try
            {
                int selectedDishId = Convert.ToInt32(cbDishes.SelectedValue);
                int selectedDrinkId = Convert.ToInt32(cbDrinks.SelectedValue);
                int selectedDessertId = Convert.ToInt32(cbDessert.SelectedValue);

                AppData.Menu selectedDish = listMenu.FirstOrDefault(c => c.MenuId == selectedDishId);
                AppData.Menu selectedDrink = listMenu.FirstOrDefault(c => c.MenuId == selectedDrinkId);
                AppData.Menu selectedDessert = listMenu.FirstOrDefault(c => c.MenuId == selectedDessertId);



                if (selectedDish != null)
                {
                    itemPrice += (double)selectedDish.MenuPrice;
                }
                if (selectedDrink != null)
                {
                    itemPrice += (double)selectedDrink.MenuPrice;
                }
                if (selectedDessert != null)
                {
                    itemPrice += (double)selectedDessert.MenuPrice;
                }

                double totalOrder = quantity * itemPrice;
                txtAddTotal.Text = currencySymbol + totalOrder.ToString("N2");

                txtAddTotal.Focus();


                using (SqlConnection con = new SqlConnection("Data Source=LAPTOP-K06JGE5P\\SQLEXPRESS;Initial Catalog=DBSYS;Integrated Security=True"))
                {
                    con.Open();


                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Purchase(ItemNo, Orders, Quantity, Price, Total, CustomerName, userId) " +
                        "VALUES (@ItemNo, @Orders, @Quantity, @Price, @Total, @CustomerName, @userId)", con))
                    {

                        cmd.Parameters.AddWithValue("@ItemNo", lblItemNo.Text);
                        cmd.Parameters.AddWithValue("@Orders", $"{selectedDish?.MenuName}, {selectedDrink?.MenuName}, {selectedDessert?.MenuName}");
                        cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                        cmd.Parameters.AddWithValue("@Price", itemPrice);
                        cmd.Parameters.AddWithValue("@Total", totalOrder);
                        cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
                        cmd.Parameters.AddWithValue("@userId", lblUserId.Text);



                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

                FrmCustomerPOS_Load(sender, e);
                dataGridViewPurchase.Refresh();

                loadCbBox();
                txtQuantity.Clear();
                MessageBox.Show("Successfully Inserted!!");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {

                e.Graphics.DrawString("KUSINIRO'S BRISTO", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, new Point(250, 20));
                e.Graphics.DrawString("M.L. Quezon National Highway, Lapu-Lapu City", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new Point(110, 60));
                e.Graphics.DrawString("6016 Lapu-Lapu City", new Font("Arial", 20, FontStyle.Regular), Brushes.Black, new Point(270, 90));
                e.Graphics.DrawString("09612748364 or 2347568", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(260, 120));
                e.Graphics.DrawString("––––––––––––––––––––––––––––––––––––––––––––––––––––", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(30, 160));

                e.Graphics.DrawString(" " + cbBoxDiningOption.Text, new Font("Arial", 24, FontStyle.Bold), Brushes.Black, new Point(320, 190));
                e.Graphics.DrawString("––––––––––––––––––––––––––––––––––––––––––––––––––––", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(30, 220));

                e.Graphics.DrawString("Cashier: Mel Anthony Rusiana ", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(40, 250));
                e.Graphics.DrawString("Price", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(510, 300));
                e.Graphics.DrawString("Quantity", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(610, 300));
                e.Graphics.DrawString("––––––––––––––––––––––––––––––––––––––––––––––––––––", new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(14, 320));


                e.Graphics.DrawString("TOTAL AMOUNT ", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(400, 880));
                e.Graphics.DrawString("CASH RECEIVED ", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(400, 910));
                e.Graphics.DrawString("CHANGE ", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(400, 940));

                e.Graphics.DrawString(txtTotalBill.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(630, 880));
                e.Graphics.DrawString(txtAmount.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(630, 910));
                e.Graphics.DrawString("₱" + txtChange.Text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, new Point(630, 940));


                for (int i = 0; i < dataGridViewPurchase.RowCount; i++)
                {
                    e.Graphics.DrawString(" " + Convert.ToString(dataGridViewPurchase.Rows[i].Cells[1].Value), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, margin, y);

                    e.Graphics.DrawString("" + Convert.ToInt32(dataGridViewPurchase.Rows[i].Cells[2].Value), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, a, y);

                    e.Graphics.DrawString("₱ " + Convert.ToDouble(dataGridViewPurchase.Rows[i].Cells[3].Value), new Font("Arial", 14, FontStyle.Bold), Brushes.Black, b, y);
                    y += lineSpacing;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("My Name", 800, 1000);
            printPreviewDialog1.ShowDialog();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                Total = CalculateTotalForCustomer(txtCustomerName.Text);

                // Check if Senior Citizen or PWD is selected in the combo box
                if (cbDiscount.SelectedItem != null)
                {
                    string discountType = cbDiscount.SelectedItem.ToString();
                    double discountPercentage = 0;

                    // Apply the discount based on the selected option
                    switch (discountType)
                    {
                        case "Senior Citizen":
                            discountPercentage = 0.05; // 5% discount for senior citizens
                            break;

                        case "Person with Disability":
                            discountPercentage = 0.07; // 7% discount for PWD
                            break;

                        // Add more cases if needed for additional discount types

                        default:
                            break;
                    }

                    // Apply the discount to the total
                    Total -= Total * discountPercentage;
                }

                txtTotalBill.Text = currencySymbol + Total.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private double CalculateTotalForCustomer(string customerName)
        {
            double total = 0;

            foreach (DataGridViewRow row in dataGridViewPurchase.Rows)
            {

                object cellValue = row.Cells[5].Value;
                if (cellValue != null)
                {
                    string rowCustomerName = cellValue.ToString();
                    double price = Convert.ToDouble(row.Cells["Total"].Value);


                    if (rowCustomerName == customerName)
                    {

                        double quantity = Convert.ToDouble(row.Cells["Quantity"].Value);


                        double subtotal = quantity * price;


                        total += subtotal;
                    }
                }

            }
            return total;
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            try
            {
                txtTotalBill.Text = Total.ToString("N2");

                double amountPaid = 0;
                if (double.TryParse(txtAmount.Text, out amountPaid))
                {
                    if (amountPaid < 0)
                    {
                        throw new Exception("Amount paid cannot be negative.");
                    }

                    if (amountPaid < Total)
                    {
                        throw new Exception("Amount paid is less than the Total Bill.");
                    }

                    // Check if Senior Citizen or PWD is selected in the combo box
                    if (cbDiscount.SelectedItem != null)
                    {
                        string discountType = cbDiscount.SelectedItem.ToString();
                        double discountPercentage = 0;

                        // Apply the discount based on the selected option
                        switch (discountType)
                        {
                            case "Senior Citezen":
                                discountPercentage = 0.05; // 5% discount for senior citizens
                                break;

                            case " Person with Disability":
                                discountPercentage = 0.07; // 7% discount for PWD
                                break;

                            // Add more cases if needed for additional discount types

                            default:
                                break;
                        }

                        // Calculate the discounted total
                        double discountedTotal = Total - (Total * discountPercentage);

                        // Deduct the discounted total from the amount paid
                        double change = amountPaid - discountedTotal;

                        txtChange.Text = currencySymbol + change.ToString("N2");
                    }
                    else
                    {
                        // If no discount is selected, deduct the original total from the amount paid
                        double change = amountPaid - Total;
                        txtChange.Text = currencySymbol + change.ToString("N2");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input for amount paid.");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid input format: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
