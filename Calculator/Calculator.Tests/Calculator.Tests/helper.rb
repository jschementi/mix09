include System::Windows

$calc = Application.current.root_visual.find_name 'Calculator'

#
# Extend the Calculator instance with 
# helper methods to click buttons
#
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

  #
  # Do a list of calculator commands
  #
  #   do 1, 'Add', 2, 'equals'
  #
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

#
# Run code with file-system operations pointed
# at the application's xap file, since the tests
# run in their own xap
# 
#   run_from_application { require 'foo' }
#
def run_from_application
  tests = DynamicApplication.xap_file
  DynamicApplication.xap_file = nil
  yield
  DynamicApplication.xap_file = tests
end

$calc.clear

