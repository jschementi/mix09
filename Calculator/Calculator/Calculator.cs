using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NumberProject;

namespace CalculatorProject
{
    [TemplatePart(Name = "ScreenTextBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "OneButton", Type = typeof(Button))]
    [TemplatePart(Name = "TwoButton", Type = typeof(Button))]
    [TemplatePart(Name = "ThreeButton", Type = typeof(Button))]
    [TemplatePart(Name = "FourButton", Type = typeof(Button))]
    [TemplatePart(Name = "FiveButton", Type = typeof(Button))]
    [TemplatePart(Name = "SixButton", Type = typeof(Button))]
    [TemplatePart(Name = "SevenButton", Type = typeof(Button))]
    [TemplatePart(Name = "EightButton", Type = typeof(Button))]
    [TemplatePart(Name = "NineButton", Type = typeof(Button))]
    [TemplatePart(Name = "ZeroButton", Type = typeof(Button))]
    [TemplatePart(Name = "AddButton", Type = typeof(Button))]
    [TemplatePart(Name = "SubtractButton", Type = typeof(Button))]
    [TemplatePart(Name = "MultiplyButton", Type = typeof(Button))]
    [TemplatePart(Name = "DivideButton", Type = typeof(Button))]
    [TemplatePart(Name = "EqualsButton", Type = typeof(Button))]
    [TemplatePart(Name = "ClearButton", Type = typeof(Button))]
    [TemplatePart(Name = "NegateButton", Type = typeof(Button))]
    [TemplatePart(Name = "FloatButton", Type = typeof(Button))]
    public class Calculator : Control
    {
        #region Private Enumerations

        private enum CalculatorOperation
        { None, Add, Subtract, Multiply, Divide }

        #endregion

        #region Fields

        private TextBlock ScreenTextBlock;

        private Number firstNumber = new Number(0);
        private Number currentNumber = new Number(0);

        private CalculatorOperation currentOperation = CalculatorOperation.None;

        private bool isOperationButtonPressed = false;
        private bool isFloatButtonPressed = false;
        private bool isEnteringSecondNumber = false;
        private bool isCalculationDone = false;

        public Number CurrentNumber {
            get { return currentNumber; }
            set { currentNumber = value; }
        }

        #endregion

        #region Constructors

        public Calculator()
        {
            DefaultStyleKey = typeof(Calculator);

            this.KeyUp += new KeyEventHandler(Calculator_KeyUp);
        }

        #endregion

        #region Overriden Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ScreenTextBlock = GetTemplateChild("ScreenTextBlock") as TextBlock;
            if (ScreenTextBlock == null)
            {
                ScreenTextBlock = new TextBlock();
            }

            Button button;

            string[] digitButtonsNames = { "ZeroButton", "OneButton", "TwoButton", "ThreeButton",
                                               "FourButton", "FiveButton", "SixButton", "SevenButton",
                                               "EightButton", "NineButton"};

            string[] operationButtonsNames = { "AddButton", "SubtractButton", "MultiplyButton",
                                                    "DivideButton" };

            for (int i = 0; i < digitButtonsNames.Length; i++)
            {
                button = GetTemplateChild(digitButtonsNames[i]) as Button;
                if (button != null)
                {
                    button.Tag = i;
                    button.Click += new RoutedEventHandler(DigitButton_Click);
                }
            }
            foreach (string operationButtonName in operationButtonsNames)
            {
                button = GetTemplateChild(operationButtonName) as Button;
                if (button != null)
                {
                    button.Tag = Enum.Parse(typeof(CalculatorOperation),
                        operationButtonName.Substring(0, operationButtonName.IndexOf("Button")), false);
                    button.Click += new RoutedEventHandler(OperationButton_Click);
                }
            }

            button = GetTemplateChild("EqualsButton") as Button;
            if (button != null)
            {
                button.Click += new RoutedEventHandler(EqualsButton_Click);
            }

            button = GetTemplateChild("NegateButton") as Button;
            if (button != null)
            {
                button.Click += new RoutedEventHandler(NegateButton_Click);
            }

            button = GetTemplateChild("ClearButton") as Button;
            if (button != null)
            {
                button.Click += new RoutedEventHandler(ClearButton_Click);
            }

            button = GetTemplateChild("FloatButton") as Button;
            if (button != null)
            {
                button.Click += new RoutedEventHandler(FloatButton_Click);
            }
        }

        #endregion

        #region EventHandlers

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Focus();

            Button button = sender as Button;
            if (button != null && button.Tag is int)
            {
                OnDigitButtonPressed((int)button.Tag);
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            this.Focus();

            Button button = sender as Button;
            if (button != null && button.Tag is CalculatorOperation)
            {
                OnOperationButtonPressed((CalculatorOperation)button.Tag);
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Focus();

            OnEqualsButtonPressed();
        }

        private void NegateButton_Click(object sender, RoutedEventArgs e)
        {
            this.Focus();

            OnNegateButtonPressed();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.Focus();

            OnClearButtonPressed();
        }

        private void FloatButton_Click(object sender, RoutedEventArgs e)
        {
            this.Focus();

            OnFloatButtonPressed();
        }

        private void Calculator_KeyUp(object sender, KeyEventArgs e)
        {
            this.Focus();

            switch (e.Key)
            {
                case Key.D0:
                    OnDigitButtonPressed(0);
                    e.Handled = true;
                    return;
                case Key.D1:
                    OnDigitButtonPressed(1);
                    e.Handled = true;
                    return;
                case Key.D2:
                    OnDigitButtonPressed(2);
                    e.Handled = true;
                    return;
                case Key.D3:
                    OnDigitButtonPressed(3);
                    e.Handled = true;
                    return;
                case Key.D4:
                    OnDigitButtonPressed(4);
                    e.Handled = true;
                    return;
                case Key.D5:
                    OnDigitButtonPressed(5);
                    e.Handled = true;
                    return;
                case Key.D6:
                    OnDigitButtonPressed(6);
                    e.Handled = true;
                    return;
                case Key.D7:
                    OnDigitButtonPressed(7);
                    e.Handled = true;
                    return;
                case Key.D8:
                    OnDigitButtonPressed(8);
                    e.Handled = true;
                    return;
                case Key.D9:
                    OnDigitButtonPressed(9);
                    e.Handled = true;
                    return;
                case Key.C:
                    OnClearButtonPressed();
                    e.Handled = true;
                    return;
                case Key.A:
                    OnOperationButtonPressed(CalculatorOperation.Add);
                    e.Handled = true;
                    return;
                case Key.S:
                    OnOperationButtonPressed(CalculatorOperation.Subtract);
                    e.Handled = true;
                    return;
                case Key.M:
                    OnOperationButtonPressed(CalculatorOperation.Multiply);
                    e.Handled = true;
                    return;
                case Key.D:
                    OnOperationButtonPressed(CalculatorOperation.Divide);
                    e.Handled = true;
                    return;
                case Key.N:
                    OnNegateButtonPressed();
                    e.Handled = true;
                    return;
                case Key.E:
                    OnEqualsButtonPressed();
                    e.Handled = true;
                    return;
                case Key.F:
                    OnFloatButtonPressed();
                    e.Handled = true;
                    return;
            }
        }

        #endregion

        #region Private Methods

        public void OnValueChanged()
        {
            ScreenTextBlock.Text = currentNumber.ToString();
        }
        
        private void OnDigitButtonPressed(int digit)
        {
            if (isOperationButtonPressed)
            {
                isOperationButtonPressed = false;
                isCalculationDone = false;
                isFloatButtonPressed = false;
                isEnteringSecondNumber = true;
                firstNumber = new Number(currentNumber);
                currentNumber = new Number(digit);
            }
            else if ((currentNumber == 0 && currentNumber.DigitsCount == 1 && !isFloatButtonPressed)
                     || isCalculationDone)
            {
                isCalculationDone = false;
                isFloatButtonPressed = false;
                currentNumber = new Number(digit);
            }
            else
            {
                if (isFloatButtonPressed)
                {
                    isFloatButtonPressed = false;
                    currentNumber = Number.AppendLastDigit(currentNumber, digit, true);
                }
                else
                {
                    currentNumber = Number.AppendLastDigit(currentNumber, digit);
                }
            }
            OnValueChanged();
        }

        private void OnOperationButtonPressed(CalculatorOperation operation)
        {
            if (isEnteringSecondNumber)
            {
                OnEqualsButtonPressed();
            }

            currentOperation = operation;
            isOperationButtonPressed = true;
            isFloatButtonPressed = false;
        }

        private void OnEqualsButtonPressed()
        {
            if (!(currentOperation == CalculatorOperation.None || isOperationButtonPressed == true))
            {
                switch (currentOperation)
                {
                    case CalculatorOperation.Add:
                        currentNumber += firstNumber;
                        break;
                    case CalculatorOperation.Subtract:
                        currentNumber = firstNumber - currentNumber;
                        break;
                    case CalculatorOperation.Multiply:
                        currentNumber *= firstNumber;
                        break;
                    case CalculatorOperation.Divide:
                        if (currentNumber != 0)
                        {
                            currentNumber = firstNumber / currentNumber;
                        }
                        else
                        {
                            ScreenTextBlock.Text = "Can't divide by zero.";
                        }
                        break;
                    default:
                        throw new NotSupportedException(String.Format("Unsupported operation: {0}",
                            Enum.GetName(typeof(CalculatorOperation), currentOperation)));
                }
                if (!(currentOperation == CalculatorOperation.Divide && currentNumber == 0))
                {
                    OnValueChanged();
                }
                currentOperation = CalculatorOperation.None;
                isCalculationDone = true;
            }
        }

        private void OnNegateButtonPressed()
        {
            currentNumber = -currentNumber;
            OnValueChanged();
        }

        private void OnClearButtonPressed()
        {
            firstNumber = new Number(0);
            currentNumber = new Number(0);
            isOperationButtonPressed = false;
            isFloatButtonPressed = false;
            isEnteringSecondNumber = false;
            isCalculationDone = false;
            currentOperation = CalculatorOperation.None;
            OnValueChanged();
        }

        private void OnFloatButtonPressed()
        {
            isFloatButtonPressed = true;
        }

        #endregion
    }
}
