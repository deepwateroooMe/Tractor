using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace Kuaff.Tractor {

    static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new MainForm()); // 表单游戏，进入主界面
        }
    }
}

