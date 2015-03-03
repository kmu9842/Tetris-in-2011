using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tetris
{
    internal static class Program
    {
        internal static frm_main Ifrm_main;
        internal static frm_Setting Ifrm_set;
        internal static frm_newgame Ifrm_newgame;
        internal static frm_rank Ifrm_rank;
        internal static bool SoundEffect;

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Ifrm_main = new frm_main();
            Ifrm_set = new frm_Setting();
            Ifrm_newgame = new frm_newgame();
            Ifrm_rank = new frm_rank();
            SoundEffect = true;
            Application.Run(Ifrm_main);
        }
    }
}