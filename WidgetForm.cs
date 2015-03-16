﻿/* ------------------------------------------------------------------------- */
///
/// WidgetForm.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Cube.Extensions.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Forms.WidgetForm
    /// 
    /// <summary>
    /// Widget アプリケーション用のフォームを作成するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class WidgetForm : Form
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// WidgetForm
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public WidgetForm()
            : base()
        {
            AutoScaleMode = AutoScaleMode.None;
            DoubleBuffered = true;
            Font = new Font(Font.Name, 12, Font.Style, GraphicsUnit.Pixel);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            SizeGripStyle = SizeGripStyle.Hide;
        }

        #endregion

        #region Hiding properties

        [Browsable(false)]
        public new AutoScaleMode AutoScaleMode
        {
            get { return base.AutoScaleMode; }
            set { base.AutoScaleMode = value; }
        }

        #endregion

        #region Override properties and methods

        /* ----------------------------------------------------------------- */
        ///
        /// CreateParams
        /// 
        /// <summary>
        /// コントロールの作成時に必要な情報をカプセル化します。
        /// WidgetForm クラスでは、フォームに陰影を付与するための
        /// パラメータをベースの値に追加しています。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ClassStyle |= 0x00020000;
                return cp;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnMouseDown
        /// 
        /// <summary>
        /// マウスクリック時に発生するイベントです。
        /// </summary>
        /// 
        /// <remarks>
        /// ドラッグ中のマウス移動にフォームを追随させるうにカスタマイズ
        /// します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Win32Api.ReleaseCapture();
                Win32Api.SendMessage(Handle, Win32Api.WM_NCLBUTTONDOWN,
                    (IntPtr)Win32Api.HT_CAPTION, IntPtr.Zero);
            }
            base.OnMouseDown(e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnControlAdded
        /// 
        /// <summary>
        /// コントロールが追加された時に発生するイベントです。
        /// </summary>
        /// 
        /// <remarks>
        /// 追加されたコントロールに対しても、ドラッグ中のマウス移動に
        /// フォームを追随させるためのイベントハンドラを設定します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnControlAdded(ControlEventArgs e)
        {
            AddMouseDown(e.Control);
            base.OnControlAdded(e);
        }


        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// AddMouseDown
        /// 
        /// <summary>
        /// コントロールに対して、ドラッグ中のマウス移動に
        /// フォームを追随させるためのイベントハンドラを設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddMouseDown(Control control)
        {
            foreach (Control child in control.Controls) AddMouseDown(child);
            if (MouseDownAvailable(control))
            {
                control.MouseDown += (s, e) => OnMouseDown(e);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseDownAvailable
        /// 
        /// <summary>
        /// MouseDown イベントに対して、ドラッグ中のマウス移動にフォームを
        /// 追随させるためのハンドラを追加して良いかどうかを判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool MouseDownAvailable(Control control)
        {
            var reserved = control.HasEventHandler("MouseEnter") ||
                           control.HasEventHandler("MouseHover") ||
                           control.HasEventHandler("MouseLeave") ||
                           control.HasEventHandler("MouseDown") ||
                           control.HasEventHandler("MouseUp") ||
                           control.HasEventHandler("MouseClick") ||
                           control.HasEventHandler("MouseDoubleclick");
            return IsContainerControl(control) && !reserved;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsContainerControl
        /// 
        /// <summary>
        /// MouseDown イベントを奪っても良いコントロールかどうかを
        /// 判別します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool IsContainerControl(Control control)
        {
            if (control is ContainerControl ||
                control is Panel ||
                control is GroupBox ||
                control is Label ||
                control is PictureBox) return true;
            return false;
        }

        #region Win32 APIs

        internal class Win32Api
        {
            public const int WM_NCLBUTTONDOWN = 0xA1;
            public const int HT_CAPTION = 0x2;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool ReleaseCapture();
        }

        #endregion

        #endregion
    }
}
