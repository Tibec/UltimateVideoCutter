   M S B A M L   `   `   `  ����J  FVideoCutterMax2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:VideoCutterMax2.ViewModelVideoCutterMax2.ViewModel  5      ;"clr-namespace:VideoCutterMax2.ViewVideoCutterMax2.View  5      "    VideoCutterMax2.View.CutList   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6mv'clr-namespace:VideoCutterMax2.ViewModel     5      4local"clr-namespace:VideoCutterMax2.View     5        � DataContext  5      E'clr-namespace:VideoCutterMax2.ViewModelVideoCutterMax2.ViewModel  5      0  *VideoCutterMax2.ViewModel.CutListViewModel  6
   6vm'clr-namespace:VideoCutterMax2.ViewModel     6   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� -   5         ��Name$ listAdd��5      $��Ajout��6"   $
��Left=�62   $	��Top=�6M   $��75��6e   $��0��6p   ��6}   �� *
AddCommand+�� 5   
   -   5        $ 
listDelete��5      $��Retirer��6%   $��Right=�67   $	��Top=�6S   $��75��6k   $��0��6v   {�6�   �� *	DelEnable+��6�   �� *
DelCommand+�� 5   
   -   5         ��Name$ cutList��5      $��0��6$    � IsSharedSizeScope . $��12.2,31,6,10q�6t   $��0q�6�   $	��Top=�6�   $��Disabled=�6�   $��Center=�6�    � UseLayoutRounding .  ��6   �� *
CutsList+��5      �� 5      .R�5      � $��Stretch=�6   $��Stretch=�69   ��6U   �� *ActualWidth+ �� ElementName$ cutList�� �� Mode$ OneWay=� � ColumnDefinitions 5      �� 5      $��* �60   �� 5      $��* �60   �� 5      $��* �60   .+�5!      �� -   5        $��0��5!   T   $��Stretch=�6d   ��6"   �� *Name+	 �� ValidatesOnDataErrors	 . 6�   �� 5"      
 ��Name$
 Begin��6"   -   5        $��1��5"   J   $��Center=�6Z    � ToolTip$" Respectez le format 00:00:00��6w   ��62   �� *Begin+6�   �� 5#      $	
 End��6"   -   5        $��2��5#   E   $��Center=�6U   $" Respectez le format 00:00:00��6r   ��6/   �� *End+6�   5$      5%      ��5(      �� 5)       �� 
TargetType" ��6   .�5*      -   5        5+      5.      �� 5/   
   -   5        $ Exporter��5/      $��Exporter��6#   $��Right=�66   $	��Top=�6R   $��75��6j   $��1��6u   {�6�   �� *IsExportable+��6�   �� *ExportCommand+50      53      54      