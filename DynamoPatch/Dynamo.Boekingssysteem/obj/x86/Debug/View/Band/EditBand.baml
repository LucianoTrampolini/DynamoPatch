   M S B A M L   `   `   `  ����Q  MDynamo.Boekingssysteem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Dynamo.Boekingssysteem.ControlsDynamo.Boekingssysteem.Controls  5      /    )Dynamo.Boekingssysteem.View.Band.EditBand   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =dyn-clr-namespace:Dynamo.Boekingssysteem.Controls     5      .��5	      ��    SubItemBorder#��[  6   ._�5
   
   3   -Dynamo.Boekingssysteem.Controls.AnimatedPanel  $��0q�6   $	��620��6'     �� Orientation$  
Horizontal=�66   .+�5        $��5q�6    $	��300��6+   .+�5        $  
Horizontal=�6$   .+�5      ��   DescriptionLabel#��[ 6   $
��Naam��6F   �� 5      $	��190��6t   ��6   ��  �� Path$
 Naam�4   .Dynamo.Boekingssysteem.Controls.FocusExtension   	IsFocused 69   �� $ InitialFocus�6t   5        5      $  
Horizontal=�6$   .+�5      �� #��[ 6   $��Contract per��6F   �� 5      $	��190��6@   ��6   �� $ ContractPer�{�6L   �� $ IsNieuwContract�5        5      $  
Horizontal=�6$   .+�5      �� #��[ 6   $��Einde contract��6F   �� 5      $	��190��6f   ��6   �� $ EindeContract� �� UpdateSourceTrigger$ PropertyChanged=�6f   5        5      $  
Horizontal=�6$   .+�5      �� #��[ 6   $��Oefendag��6F   �� 5       �� DisplayMemberPath$ Value��6G    �� SelectedValuePath$	 Key��6a   $	��190��6�   ��6   �� $ 
OefenDagen� �� SelectedValue 6y   �� $ OefenDag�{�6�   �� $ IsNieuwContract�5        5      $  
Horizontal=�6$   .+�5      �� #��[ 6   $��Dagdeel��6F   �� 5      $ Omschrijving��6E   $ Id��6f   $	��190��6�   ��6   �� $ Dagdelen� 6}   �� $ 	DagdeelId�{�6�   �� $ IsNieuwContract�5        5       $  
Horizontal=�6$   .+�5!      �� #��[ 6   $��Oefenruimte��6F   �� 5"      $
 Naam��6I   $ Id��6b   $	��190��6�   ��6   �� $ Oefenruimtes� 6y   �� $ OefenruimteId�{�6�   �� $ IsNieuwContract�5#        5$      $  
Horizontal=�6$   .+�5%      �� #��[ 6   $��	MaandHuur��6F   4   .Dynamo.Boekingssysteem.Controls.DecimalTextBox  5&      $��80��6I   ��6)   �� $ 	MaandHuur�6I   5'        5(      $  
Horizontal=�6$   .+�5)      �� #��[ 6   $
��Borg��6F     5*      $��80��6D   ��6)   �� $
 Borg�6D   5+        5,      $  
Horizontal=�6$   .+�5-      �� #��[ 6   $��Backline��6F   4   .Dynamo.Boekingssysteem.Controls.IntegerTextBox  5.      $��80��6H   ��6)   �� $ Backline�6H   5/        50      $  
Horizontal=�6$   .+�51      �� #��[ 6   $��Kasten��6F     52      $��80��6F   ��6)   �� $ Kasten�6F   53        54      $  
Horizontal=�6$   .+�55      �� #��[ 6   $��
Microfoons��6F     56      $��80��6J   ��6)   �� $ 
Microfoons�6J   57        58      $  
Horizontal=�6$   .+�59      �� #��[ 6   $��Extra versterkers��6F     5:      $��80��6P   ��6)   �� $ ExtraVersterkers�6P   5;        5<      $  
Horizontal=�6$   .+�5=      �� #��[ 6   $��Crash��6F   �� 5>       d� 	IsChecked 6   �� $ Crash�5?      5@        5B      $��5q�6    $	��300��6+   .+�5D      ��   HeaderLabel#��[ 6   $��Contactpersoon 1��6=   $��28��6X   $	��250��6d   $��Center=�6p     5E      $  
Horizontal=�6$   .+�5F      �� #��[ 6   $
��Naam��6F   �� 5G      $	��190��6H   ��6   �� $ Contactpersoon1Naam�6H   5H        5I      $  
Horizontal=�6$   .+�5J      �� #��[ 6   $��Adres��6F   �� 5K      $	��190��6I   ��6   �� $ Contactpersoon1Adres�6I   5L        5M      $  
Horizontal=�6$   .+�5N      �� #��[ 6   $��Plaats��6F   �� 5O      $	��190��6J   ��6   �� $ Contactpersoon1Plaats�6J   5P        5Q      $  
Horizontal=�6$   .+�5R      �� #��[ 6   $��Telefoon��6F   �� 5S      $	��190��6L   ��6   �� $ Contactpersoon1Telefoon�6L   5T        5U      $  
Horizontal=�6$   .+�5V      �� #��[ 6   $��Email��6F   �� 5W      $	��190��6I   ��6   �� $ Contactpersoon1Email�6I   5X      �� 5Y      #��[ 6   $��Contactpersoon 2��6=   $	��250��6X   $��Center=�6d     5Z      $  
Horizontal=�6$   .+�5[      �� #��[ 6   $
��Naam��6F   �� 5\      $	��190��6H   ��6   �� $ Contactpersoon2Naam�6H   5]        5^      $  
Horizontal=�6$   .+�5_      �� #��[ 6   $��Adres��6F   �� 5`      $	��190��6I   ��6   �� $ Contactpersoon2Adres�6I   5a        5b      $  
Horizontal=�6$   .+�5c      �� #��[ 6   $��Plaats��6F   �� 5d      $	��190��6J   ��6   �� $ Contactpersoon2Plaats�6J   5e        5f      $  
Horizontal=�6$   .+�5g      �� #��[ 6   $��Telefoon��6F   �� 5h      $	��190��6L   ��6   �� $ Contactpersoon2Telefoon�6L   5i        5j      $  
Horizontal=�6$   .+�5k      �� #��[ 6   $��Email��6F   �� 5l      $	��190��6I   ��6   �� $ Contactpersoon2Email�6I   5m      5p        5q      $  
Horizontal=�6    $��5q�69   .+�5r      �� #��[ 6   $��Opmerkingen��6B   �� 5s      $	��500��6<   $	��100��6H    �� AcceptsReturn . 	 �� TextWrapping$
	 Wrap=�6j   ��6   �� $ Opmerkingen�6j   5t        5v      .+�5w      �� $��2,0,2,0q�5x      ��5y      �� $ CurrentViewModel�5z        5|      $��Stretch=�6    .+�5}      ��   CommandButtons#��[ 5      $��Right=�5�      ��5~      �� $ Commands�5�      5�      5�      5�      5�      