include Microsoft::Scripting::Silverlight
include System::Windows
include System::Windows::Controls

t = TextBlock.new
t.text = "Extension Example"
c = Canvas.new
c.children.add t

Application.current.root_visual = c
