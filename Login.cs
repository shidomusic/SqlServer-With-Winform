﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
using System.Data.SqlClient;

namespace QLNhaHang
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source='DESKTOP-TR8V8B8\MSSQLSERVER2505';Initial Catalog='QLCuaHang';Integrated Security='True'");

        public static int ID;

        //Lấy ID User
        private string getID(string username, string pass)
        {
            string id = "";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ft_DangNhap('" + username + "', '" + pass + "')", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd); //Chuyển dữ liệu về
                DataTable dt = new DataTable(); //Tạo kho ảo lưu trữ dữ liệu
                da.Fill(dt); //Đổ dữ liệu vào kho
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        id = dr["ChucVu"].ToString();
                        ID = Convert.ToInt32(dr["ID"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        public static string ID_USER = "";


        private void button1_Click(object sender, EventArgs e)
        {
            ID_USER = getID(txt_username.Text, txt_pass.Text);
            if (ID_USER != "")
            {
                if (ID_USER == "Admin")
                {
                    Main login = new Main();
                    login.Show();
                    this.Hide();
                }
                if (ID_USER == "Nhân Viên")
                {
                    NhanVien login = new NhanVien();
                    login.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Tài khoản và mật khẩu không đúng!");
            }
        }

    }
}
