using System;
using System.Windows.Forms;

public static class UiHelper
{
    // Đặt nhãn ở x=15, điều khiển ở x=150, cùng hàng y
    public static void Row(Control parent, string label, Control c, int y)
    {
        parent.Controls.Add(new Label { Text = label, Left = 15, Top = y + 4, Width = 130 });
        c.Left = 150; c.Top = y;
        parent.Controls.Add(c);
    }

    public static Button Btn(Control parent, string text, int x, int y, int w, EventHandler click)
    {
        var b = new Button { Text = text, Left = x, Top = y, Width = w, Height = 30 };
        b.Click += click;
        parent.Controls.Add(b);
        return b;
    }

    public static void Info(string m)
    {
        MessageBox.Show(m, "e-SHOPPING", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    public static void Error(string m)
    {
        MessageBox.Show(m, "e-SHOPPING", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}