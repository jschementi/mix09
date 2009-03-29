include System::Windows
$page = Application.current.root_visual

describe "Tic Tac Toe" do

  after do
    $page.reset
  end

  describe "on load" do
    should 'say Play!' do
      $page.find_name('WhosMove').text.
        should.equal 'Play!'.to_clr_string
    end

    should 'have a restart button' do
      $page.find_name('Restart').content.
        should.equal 'Restart'.to_clr_string
    end

    should 'have a grid' do
      $page.find_name('XamlBoard').source.uri_source.to_string.
        should.equal '/images/Board.png'.to_clr_string
    end
  end

  describe 'on a user turn' do
    should 'show an X' do
      $page.MakeUserMove($page.find_name('R00'))

      $page.find_name('Cross00').visibility.to_string.to_s.
        should.equal 'Visible'
      [0,1,2].each do |i|
        [0,1,2].each do |j|
          ['Ellipse', 'Cross'].each do |k|
            next if i == 0 and j == 0 and k == 'Cross'
            next if i == 1 and j == 1 and k == "Ellipse" # AI move
            $page.find_name("#{k}#{i}#{j}").visibility.to_string.to_s.
              should.equal 'Collapsed'
          end
        end
      end
    end
  end
end
