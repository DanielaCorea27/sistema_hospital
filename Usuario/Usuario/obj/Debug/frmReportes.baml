   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	       Usuario.frmReportes   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Usuario     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Reportes��5	   	   $	��759��6   $
��1258��5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��101* �6   �� 5      $
��326* �6   �� 5      $
��Auto �6   �� 5      $
��163* �6   .+�5   
   �� -   5         ��Name$ btnExpedientePacientes��5      $��EXPEDIENTE DE PACIENTES��61   $��98��6S   $��108,217,420,0q�6_   $	��Top=�6v   $	��382��6�   $��25(�6�   $
��Bold"�6�   ���	#DD000000
���   �$��2��6�   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ White��6#   � 5      $ 	#FFB1C9EF��6#   	 � Offset$	 1Q�65   � 5      $ 	#FFD9E4F7��6#   $	 0.487Q�65   5      5      �� 5    
   -   5        $ btnRecetasPacientes��5       $��RECETAS POR PACIENTES��6.   $��98��6N   $��413,217,248,0q�6Z   $	��Top=�6q   $	��382��6�   $��25(�6�   $
��Bold"�6�   ���	#DD000000
���   �$��1��6�   $��3��6�   ��5!      �� 5"      $ 0.5,1&�6&   $ 0.5,0&�67   .��5#      � $ White��6#   � 5$      $ 	#FFB1C9EF��6#   $	 1Q�65   � 5%      $ 	#FFD9E4F7��6#   $	 0.487Q�65   5&      5(      �� 5*   
   -   5        $ btnExamenesPacientes��5*      $��EXAMENES POR PACIENTES��6/   $��98��6P   $��108,470,420,0q�6\   $	��Top=�6s   $	��382��6�   $��25(�6�   $
��Bold"�6�   ���	#DD000000
���   �$��2��6�   ��5+      �� 5,      $ 0.5,1&�6&   $ 0.5,0&�67   .��5-      � $ White��6#   � 5.      $ 	#FFB1C9EF��6#   $	 1Q�65   � 5/      $ 	#FFD9E4F7��6#   $	 0.487Q�65   50      52      �� 54   
   -   5        $ btnConsultasMedico��54      $��CONSULTAS POR MEDICOS��6-   $��98��6M   $��414,470,247,0q�6Y   $	��Top=�6p   $	��382��6�   $��25(�6�   $
��Bold"�6�   ���	#DD000000
���   �$��1��6�   $��3��6�   ��55      �� 56      $ 0.5,1&�6&   $ 0.5,0&�67   .��57      � $ White��6#   � 58      $ 	#FFB1C9EF��6#   $	 1Q�65   � 59      $ 	#FFD9E4F7��6#   $	 0.487Q�65   5:      5<      �� 5?   
   $��4��6   $��	0,3,0,651q�6(   ��5@      �� 5A      $ 0.5,0&�6&   $ 0.5,1&�69   .��5B      � $ #F0F3FA��6#   � 5C      $ #D5DEEF��6#   $	 1Q�63   � 5D      $ #B1C9EF��6#   $		 0.5Q�63   5E      5G      �� 5H   
   
 �� Orientation$
 
Horizontal=�6   $
��Left=�6.   $
��1000��6I   $��4��6V   $��10,-93,0,608q�6j   .+�5I      �� -   5        $ btnPacientes��5I      $��	Pacientes��6+   ���#395886
�������6h   �� 5J      -   5        $ btnHistorialesMedicos��5J      $��Historiales Médicos��64   ���#395886
�������6g   �� 5K      -   5        $ 
btnMedicos��5K      $��Médicos��6)   ���#395886
�������6P   �� 5L      -   5        $ btnConsultas��5L      $��	Consultas��6+   ���#395886
�������6T   �� 5M      -	   5        $ 
btnRecetas��5M      $��Recetas��6)   ���#395886
�������6P   �� 5N      -
   5        $ btnCitas��5N      $��Citas��6'   ���#395886
�������6L   �� 5O      -   5        $ btnExamenes��5O      $��Examenes��6*   ���#395886
�������6R   �� 5P      -   5        $ btnReportes��5P      $��Reportes��6*   ���#395886
�������6R   �� 5Q      -   5        $ btnUsuarios��5Q      $��Usuarios��6*   ���#395886
�������6R   5R      �� 5S   
   $��REPORTES��6   $
��Left=�5T      $	��Top=�5U      $��40(�5V      $��	10,48,0,0q�5W      
���   �$��2��5X      $��60��6+   $	��384��67   � 5Z   
   $��2��6   $��	0,0,630,0q�6#   .+�5[      �� -   5        $ btnHome��5[      
�������$��979,42,-856,657q�69   $��60��6R   
�������.��5\      �� $��Assets/regresar.png��6   $��50��65   $��60��6A    J� RenderTransformOrigin$ 
0.485,1.68&�6L   5]      5^      5`      5a      