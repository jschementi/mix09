require 'helper'

describe 'Calculator' do
  after do
    $calc.clear
  end

  (0..9).each do |i|
    should "show \"#{i}\" on the screen" do
      $calc.click_button i
      $calc.screen.should.equal i.to_s
    end
  end

  should 'have matching current number and screen values' do
    $calc.current_number.to_string.to_s.
      should.equal $calc.screen
  end

  should 'add' do
    $calc.do 2, 'Add', 3, 'equals'
    $calc.screen.should.equal '5'
  end

  should 'subtract' do
    $calc.do 2, 'Subtract', 3, 'equals'
    $calc.screen.should.equal '-1'
  end

  should 'multiply' do
    $calc.do 2, 'Multiply', 3, 'equals'
    $calc.screen.should.equal '6'
  end

  should 'divide' do
    $calc.do 2, 'Divide', 4, 'equals'
    $calc.screen.should.equal '0.5'
  end

  should 'negate' do
    $calc.do 2, 'negate'
    $calc.screen.should.equal '-2'
  end

  should 'input a floating point number' do
    $calc.do 2, 'float', 3
    $calc.screen.should.equal '2.3'
  end
end
