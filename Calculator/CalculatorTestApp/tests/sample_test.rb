include System::Windows

describe "Calculator" do
  before do
    @calc = Application.current.root_visual.find_name('Calculator')
  end
  
  should 'show number on screen' do
    button = @calc.get_child_element('OneButton')
    @calc.DigitButton_Click(button, nil)
    @calc.get_child_element('ScreenTextBlock').text.to_s.should.equal '1'
  end
end
