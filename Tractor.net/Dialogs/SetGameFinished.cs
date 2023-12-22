using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace Kuaff.Tractor {
	// 感觉，下面这个类，像是不曾、真正用到过一样。不明白，这个文件，在项目内部、被索引或是使用的过程与原理
    internal partial class SetGameFinished : Form {
        MainForm mainForm;

        public SetGameFinished(MainForm mainForm) {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        private void button1_Click(object sender, EventArgs e) {
            try {
                mainForm.gameConfig.WhenFinished = int.Parse(textBox2.Text);
            }
            catch(Exception ex) {
            }
        }
        private void SaveGameConfig() { // 保存【玩家、个性化配置】到文件： gameConfig. 玩家点击【保存配置】后
            Stream stream = null;
            try {
                IFormatter formatter = new BinaryFormatter(); // 不是先前的序列化，是BinaryFormatter 序列化成的 gameConfig
                stream = new FileStream("gameConfig", FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, mainForm.gameConfig); // 把这个用户自定义的游戏配置对象，序列化到文件 
            }
            catch (Exception ex) {
            }
            finally {
                if (stream != null) {
                    stream.Close();
                }
            }
        }
    }
}