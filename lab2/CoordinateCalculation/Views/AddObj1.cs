using System;
using System.Windows.Forms;
using Model;

namespace Views
{
    public partial class AddObj1 : Form
    {
        /// <summary>
        /// Переменная ссылка на абстрактный класс  Model.Movement
        /// </summary>
        public Model.Movement CurrentMove = null;

        public AddObj1 ( )
        {
            InitializeComponent ( );
            comboBoxType.SelectedIndex = 0;
#if !DEBUG
            button1.Visible = false;
#endif
        }

        /// <summary>
        /// Кнопка Random
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click ( object sender, EventArgs e )
        {
            Random rnd = new Random();

            // Случайный тип
            comboBoxType.SelectedIndex = rnd.Next ( 3 );

            // Случайные данные
            textBoxFirstPoint.Text = Math.Round( rnd.NextDouble ( ) * 200.0 - 100.0 ,1).ToString ( );
            textBoxSpeed.Text = Math.Round( rnd.NextDouble ( ) * 100.0 + 1.0 ,1).ToString ( );
            textBoxPeriod.Text = Math.Round( rnd.NextDouble ( ) * 100.0 + 1.0, 1).ToString ( );
            textBox_other.Text = Math.Round( rnd.NextDouble ( ) * 100.0 + 1.0, 1).ToString ( );
        }

        /// <summary>
        /// Кнопка Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click ( object sender, EventArgs e )
        {
            CurrentMove = null;
            Close ( );
        }

        /// <summary>
        /// Метод преобразования строки в тип double
        /// </summary>
        /// <param name="str">строка</param>
        /// <param name="value">результат</param>
        /// <returns></returns>
        private bool GetDoubleFromString ( String str, ref double value )
        {
            if ( String.IsNullOrWhiteSpace ( str ) ) return false;

            try
            {
                value = Double.Parse ( str );
            }
            catch ( Exception exception )
            {
                MessageBox.Show ( @"Invalid value entered!", @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                return false;
            }

            return true;
        }

        /// <summary>
        /// Кнопка Ок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click ( object sender, EventArgs e )
        {
            double firstPoint = 0, speed = 0, period = 0;

            if ( !GetDoubleFromString ( textBoxFirstPoint.Text, ref firstPoint ) )
            {
                textBoxFirstPoint.Focus ( );
                return;
            }

            if ( !GetDoubleFromString ( textBoxSpeed.Text, ref speed ) )
            {
                textBoxSpeed.Focus ( );
                return;
            }
            if (speed > 300000000)
            {
                textBoxSpeed.Focus();
                MessageBox.Show( "Скорость не может быть больше скорости света");
                return;
            }
            if ( !GetDoubleFromString ( textBoxPeriod.Text, ref period ) )
            {
                textBoxPeriod.Focus ( );
                return;
            }

            // Равномерное движение
            if ( comboBoxType.SelectedIndex == 0 )
            {
                try
                {
                    CurrentMove = new UniformMove ( );
                    CurrentMove.StartPoint = firstPoint;
                    CurrentMove.Speed = speed;
                    CurrentMove.Period = period;
                }
                catch ( ArgumentException ex )
                {
                    MessageBox.Show ( ex.Message, @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error );
                    CurrentMove = null;
                    return;
                }
            }

            // Равноускоренное движение
            if ( comboBoxType.SelectedIndex == 1 )
            {
                double accel = 0;
                if ( !GetDoubleFromString ( textBoxPeriod.Text, ref accel ) )
                {
                    textBoxPeriod.Focus ( );
                    return;
                }
                try
                {
                    CurrentMove = new UniformAccek ( );
                    ( ( UniformAccek ) CurrentMove ).Acceleration = accel;
                }
                catch ( ArgumentException ex )
                {
                    MessageBox.Show ( ex.Message, @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error );
                    CurrentMove = null;
                    return;
                }
            }
            // Колебательное движение
            if ( comboBoxType.SelectedIndex == 2 )
            {
                double amplitude = 0;
                if ( !GetDoubleFromString ( textBoxPeriod.Text, ref amplitude ) )
                {
                    textBoxPeriod.Focus ( );
                    return;
                }
                try
                {
                    CurrentMove = new Hesitation ( );
                    ( ( Hesitation ) CurrentMove ).Amplitude = amplitude;
                }
                catch ( ArgumentException ex )
                {
                    MessageBox.Show ( ex.Message, @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error );
                    CurrentMove = null;
                    return;
                }
            }

            try
            {
                CurrentMove.StartPoint = firstPoint;
                CurrentMove.Speed = speed;
                CurrentMove.Period = period;
            }
            catch ( ArgumentException ex )
            {
                MessageBox.Show ( ex.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error );
                CurrentMove = null;
                return;
            }
            Close ( );
        }

        /// <summary>
        /// Выбран тип движения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxType_SelectedIndexChanged ( object sender, EventArgs e )
        {
            switch ( comboBoxType.SelectedIndex )
            {
                case 0:
                    label5.Visible = false;
                    textBox_other.Visible = false;
                    break;
                case 1:
                    label5.Text = "Ускорение, м/с2";
                    label5.Visible = true;
                    textBox_other.Visible = true;
                    break;
                case 2:
                    label5.Text = "Амплитуда, м";
                    label5.Visible = true;
                    textBox_other.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Нажата клавиша при вводе в текстовое поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFirstPoint_KeyPress ( object sender, KeyPressEventArgs e )
        {
            // Можно вводить цифры, запятую и Backspace и минус в начале первого поля
            if (((System.Windows.Forms.Control)sender).Name == "textBoxFirstPoint")
                if (((System.Windows.Forms.TextBoxBase)sender).SelectionStart==0)
                    if (e.KeyChar == '-')
                        return;
            if ( Char.IsNumber ( e.KeyChar ) |
                 ( e.KeyChar == Convert.ToChar ( "," ) ) |
                 e.KeyChar == '\b' ) return;
            e.Handled = true;
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }
    }
}
