   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	       Usuario.frmInicio   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Usuario     5   	     � Title$  Menú de Inicio��5      $	��750��6(   $
��1100��65    � 
ResizeMode$ NoResize=�6B    � WindowStartupLocation$ CenterScreen=�6X    � WindowStyle$
 None=�6}   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $
��167* �6   .+�5   
   �� $��2��6   $��	0,3,0,617q�6(   ��5      �� 5       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5      �  � Color$ #F0F3FA��6#   � 5      $ #D5DEEF��6#    � Offset$ 1Q�63   � 5      $ #B1C9EF��6#   $	 0.5Q�63   5      5      �� 5   
   	 �� Orientation$	 
Horizontal=�6   $
��Left=�6.   $
��1000��6I   $��2��6V   $��
0,-690,0,0q�6j   .+�5      �� -   5        $��btnPacientes��5      $��	Pacientes��6)   ���#395886
�������6f   �� 5      -   5        $��btnHistorialesMedicos��5      $��Historiales Médicos��62   ���#395886
�������6y   �� 5      -   5        
 ��Name$
 
btnMedicos��5      $��Médicos��6)   ���#395886
�������6d   �� 5       -   5        $��btnConsultas��5       $��	Consultas��6)   ���#395886
�������6f   �� 5!      -   5        $��
btnRecetas��5!      $��Recetas��6'   ���#395886
�������6b   �� 5"      -   5        $��btnCitas��5"      $��Citas��6%   ���#395886
�������6^   �� 5#      -   5        $
 btnExamenes��5#      $��Examenes��6*   ���#395886
�������6f   �� 5$      -   5        $��btnReportes��5$      $��Reportes��6(   ���#395886
�������6d   �� 5%      -	   5        $
 btnUsuarios��5%      $��Usuarios��6*   ���#395886
�������6f   5'      �� 5(   
   $��Menú de Inicio��6   $
��Left=�5)      $	��Top=�5*      $��40(�5+      $��	10,60,0,0q�5,      
���   �$��2��5-      � 5.   
   $��2��6   .+�5/      �� $��HOSPITAL PLUS��6   $��Center=�6,   $��	0,534,0,0q�6I   $	��Top=�6\   $��72(�6t   $��Gabriola)�6�   $	��107��6�   $	��406��6�   �� 50      $	��500��6   $��-13,107,-13,0q�6!   $��/Assets/fondo.png��68   $	��Top=�6S   51      � 53   
   .+�54      �� -
   5        $
 btnHome��54      $��50��6&   
�������$��974,9,-906,691q�6E   
�������.��55      �� $��Assets/exit.png��6   $��50��61   $��60��6=   56      57      59      5:      