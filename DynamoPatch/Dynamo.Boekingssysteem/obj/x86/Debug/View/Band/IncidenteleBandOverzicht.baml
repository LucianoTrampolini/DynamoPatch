   M S B A M L   `   `   `  ����Q  MDynamo.Boekingssysteem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Dynamo.Boekingssysteem.ControlsDynamo.Boekingssysteem.Controls  5      ?    9Dynamo.Boekingssysteem.View.Band.IncidenteleBandOverzicht   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dyn-clr-namespace:Dynamo.Boekingssysteem.Controls     5        � 	Resources  5       	  Bands �� Source �� Path��%:   &	        �� 5   
    5
      �� $ 	AlleBands�5      .��5      � $��4q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��5   	   $
��Auto��5   	   ._�5      � $��
10,10,10,0q�6   $
��Auto��6'   $
��Auto��65    � RowDefinitions 5      �� 5      $
��320* �6$   �� 5      $��40 �6$   .+�5      �� $��0��5        � DataContext# [  5!        ListViewItemStyle#��[ 5"      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5$      H   BDynamo.Boekingssysteem.Controls.SelectorDoubleClickCommandBehavior   HandleDoubleClick . ��5#      ��    TheCommandToRun 5&      �� $ DefaultCommand�	 �� View	 5(      �� 5)      .P�5*      �� $
��Naam��5+      
 �� DisplayMemberBinding
 5,      �� $
 Naam��� 5.      $��Telefoon��5/      
 50      �� $ Telefoon�53      55      �� 59      $��1��6!     CommandButtons#��[ 5;      $��Stretch=�5<      ��5:      �� $ Commands�5<      5@      5A      �� 5B   
   $��2��5C   	   ._�5F      �� ��5G      �� *CurrentViewModel+5H      5I      5J      