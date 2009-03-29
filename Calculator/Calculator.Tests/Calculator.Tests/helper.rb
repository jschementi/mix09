include System::Windows

$calc = Application.current.root_visual.find_name 'Calculator'
class << $calc 
  NUMBERS = %W(Zero One Two Three Four Five Six Seven Eight Nine)

  def click_button(num_or_op)
    num_or_op = NUMBERS[num_or_op] if num_or_op.kind_of?(Integer)
    num_or_op = begin
      NUMBERS[System::Int32.parse(num_or_op)]
    rescue
      num_or_op
    end
    raise "Argument is nil" unless num_or_op

    button = get_child_element "#{num_or_op}Button"
    raise "\"#{num_or_op}\" button is not found" unless button

    method = NUMBERS.include?(num_or_op) ? :Digit : :Operation
    send("#{method}Button_Click", button, nil)
  end

  [:equals, :negate, :clear, :float].each do |name|
    define_method(name) do
      send("#{name.to_s.capitalize}Button_Click", nil, nil)
    end
  end

  def screen
    get_child_element('ScreenTextBlock').text.to_s
  end

  def do(*ops)
    ops.each do |op|
      if respond_to?(op.to_s)
        send(op)
      else
        click_button op
      end
    end
  end
end

$calc.clear

