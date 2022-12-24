using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanAn
{
    class MenuDAO
    {
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }
        public MenuDAO() { }
        public List<Menu> themMenu(string query)
        {
            List<Menu> Menu= new List<Menu>();
            DataTable data = new DataTable();
            data = Database.Instance.KetNoiSql(query);

            foreach (DataRow drow in data.Rows)
            {
               Menu Me_nu = new Menu(drow);
                Menu.Add(Me_nu);
            }
            data.Clear();
            return Menu;
        }
    }
}
