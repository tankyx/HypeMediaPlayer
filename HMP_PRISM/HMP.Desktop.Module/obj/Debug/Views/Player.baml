   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	    HMP.Desktop.Module%   HMP.Desktop.Module.Views.Player   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F   ���	#FF0A0A0A.��5      � -   5        $��maingrid��5        � ColumnDefinitions  5	   
   �� 5
      $
��266* �6   �� 5      $
��251* �6   .+�5      r� -   5        $��player��5      $y�Visible=�6I    r� Stretch$ Uniform=�6�    r� ScrubbingEnabled .  r� LoadedBehavior$ Manual=�6�    r� UnloadedBehavior$
 Stop=�6  $��2��6+   r� Source 6)   �� *SourceToPlay+ r� Volume 6}   ��  �� ElementName$ volume�� �� Path$ Value�6+  � 5   
   
������ $��2��6(   $��	0,0,0.4,0q�6<   $��45��6O   $��Bottom=�6[     5      �� 5      $��25 �6#   �� 5      ��5      �� 5      	 �� 
TargetType"	 �6   
 �� Triggers
 5      P� 5       P� Property �@J�IsMouseOver P� Value .  . �5      ��  �� Property �@J�Opacity �� Value$ 0Q�68   6D   5      P� 5       �@J�IsMouseOver . . �5      ��  �@J�Opacity$ 1Q�68   6D   5      5      .+�5      �� -   5        $��
buttonPlay��5      $
��Play��6�    � Cursor$
 Hand��6�    J� RenderTransformOrigin$ 
1.861,2.12&�6�   $��27��6�   $��Bottom=�6�   $��180.2,0,0,18q�6�   ���	#7FDDDDDD$��1��6  $
��Left=�6)  $��75��6D  ��6'   �� *PlayMediaCmd+��6H   �� $ player�� �� Mode$ OneWay=�6D  �� 5       -   5        $��buttonPause��5       $��Pause��6�   $
 Hand��6�   $ 
1.861,2.12&�6�   $��27��6�   $��Bottom=�6�   $��1��6�   $��100.2,0,0,18q�6�   ���	#7FDDDDDD$
��Left=�6,  $��75��6G  ��6(   �� *PauseMediaCmd+��6J   �� $ player��$ OneWay=�6G  �� 5!      -   5        $��volume��5!       �� IsMoveToPointEnabled . $��260.2,0,0,28q�6?   $��Bottom=�6U    � Maximum$ 1Q�6p   $
��Left=�6|   $��75��6�   $��1��6�   ���	#00000000 J� Opacity$	 0.5Q�6�    � Value$	 0.5Q�6�   �� 5"      -   5        $��posMedia��5"       . $��Bottom=�6f   $��2��6�   $	 0.5Q�6�   �� 5#      -   5        $��
buttonOpen��5#      $��Open ...��6K   $��Bottom=�6^   $��27��6y   $	 0.5Q�6�   $ 0.869,2.105&�6�   $��25.2,0,0,18q�6�   $��1��6�   $
��Left=�6�   $��70��6�   ��6'   �� *ChangeSourceCmd+6�   �� 5$      -   5        $��
buttonNext��5$      $
��Next��6'   $��1��6W   $
��Left=�6g   $��387.2,0,0,0q�6�   $	��Top=�6�   $��42��6�   $	 0.5Q�6�   ��66   �� *NextMediaCmd+6�   �� 5%      $
��Prev��6   $��1��6E   $
��Left=�6U   $��340.2,0,0,0q�6p   $	��Top=�6�   $��42��6�   $	 0.5Q�6�   ��6$   �� *PrevMediaCmd+6�   �� 5&      $
��Left=�68   
�������$��434.2,0,-0.4,18q�6m   $��27��6�   $��Bottom=�6�   $	 0.5Q�6�   $��12(�6�   $
��Bold"�6�   $��1��6�   ��6   �� *CurrentFileName+6�   5'      5(      5)      