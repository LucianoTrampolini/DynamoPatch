   M S B A M L   `   `   `  ����Q  MDynamo.Boekingssysteem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Dynamo.Boekingssysteem.ControlsDynamo.Boekingssysteem.Controls  5      7    1Dynamo.Boekingssysteem.View.Bericht.MemoOverzicht   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      =dyn-clr-namespace:Dynamo.Boekingssysteem.Controls     5        � 	Resources  5       	  Memos �� Source �� Path?   9Dynamo.Boekingssysteem.Controls.BoolToFontWeightConverter  fontWeightConverter��%[   &	        &	 4     �� 5   
    5
      �� $ AlleBerichten�  5   
   6(   5      .��5      � $��4q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��5   	   $
��Auto��5   	   ._�5      � $��
10,10,10,0q�6   $
��Auto��6'   $
��Auto��65    � RowDefinitions 5      �� 5      $��* �6$   �� 5      $��40 �6$   .+�5      �� $��0��5       � DataContext# [  5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5"      H   BDynamo.Boekingssysteem.Controls.SelectorDoubleClickCommandBehavior   HandleDoubleClick . ��5!      ��    TheCommandToRun 5$      �� $ DefaultCommand�	 �� View	 5&      �� 5'      .P�5(      �� $��Datum��5)      
 �� DisplayMemberBinding
 5*      �� $ Datum��� 5.      $��Omschrijving��5/      
 50      �� $ Titel��� 52      $	��Van��53      
 54      �� $ AangemaaktDoor�57      ��59      �� 5:       �� 
TargetType" ��6    .�5;      ��  �� Property � �� �� Value$ Stretch=�6K   �� 5<       � ��$ 14(�69   �� 5=       �@��
IsSelected 6;   �� $ 
IsSelected� �� Mode$ TwoWay=� �� Triggers 5>      ]� 5?   "    ]� 
Conditions 5@   &   �� 5A   *    �� Property �@��AlternationIndex �� Value$ 0��6]   �� 5B   *    �@��
IsSelected . 6J   ./�5D   &   ��  � ��  KleurSelecteren# �  6C   5E   #   �� 5F   "    �� Binding 6.   �� $ IsBold� �� Value true6N   .a�5G   &   ��  � ��$
 Bold"�6C   5H   #   5J      5L      �� 5P      $��1��6!     CommandButtons#��[ 5R      $��Stretch=�5S      ��5Q      �� $ Commands�5S      5W      5X      �� 5Z   
   $��1��5[      $��1��6   $	��Top=�5]      $	��Top=�5^      ��5\      �� *CurrentViewModel+5^      5`      5a      