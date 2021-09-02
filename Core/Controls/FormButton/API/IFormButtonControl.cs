﻿using BinanceTrackerDesktop.Core.Controls.API;
using BinanceTrackerDesktop.Forms.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Controls.FormButton.API
{
    public interface IFormButtonControl : IFormClickEventListenerHandle, IFormTextColorControl
    {
        Button Button { get; }
    }

    public class FormButtonControl : FormComponentClickListener, IFormButtonControl
    {
        public Button Button { get; }

        public IFormEventListener ClickEvent => base.ClickEventListener;



        public FormButtonControl(Button button, IFormEventListener clickEventListener) : base(clickEventListener)
        {
            if (button == null)
                throw new ArgumentNullException(nameof(button));

            Button = button;
        }



        public void SetTextSync(string content, Color color = default)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            SetText(content);

            if (color != default)
                SetTextColor(color);
        }

        public void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            Button.Text = content;
        }

        public void SetTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            Button.ForeColor = color;
        }
    }
}