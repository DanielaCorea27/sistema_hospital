   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	       Usuario.frmPrueba   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   !localclr-namespace:Usuario  5   	     � Title$  Gestión de Citas��5	      $	��750��6*   $
��1100��67    � 
ResizeMode$ NoResize=�6D    � WindowStartupLocation$ CenterScreen=�6Z    � WindowStyle$
 None=�6   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $
��167* �6   .+�5   
   �� $��2��6   $	��101��6(   $	��Top=�65   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 0,1&�67   .��5      �  � Color$ #B1C9EF��6#    � Offset$	 0.0Q�63   � 5      $ #D5DEEF��6#   $	 1.0Q�63   5      5      �� 5   
   $��Gestión de Citas��6   $
��Left=�5      $	��Top=�5      $��40(�5      $��	10,20,0,0q�5      
���   �$��2��5      �� 5!   
   $��Detalles de la Cita��6   $��0,101,155,507q�60   $��15q�6G   $��2��6T   ��5"      �� 5#      .R�5$      �� 
�������$��16(�6D   $
��Bold"�6R   	 �� Padding$	 5q�6d   ��6    �� 6d   5%      .��5(      � 
 � RowDefinitions
 5)      �� 5*      $
��Auto �6$   �� 5+      $
��Auto �6$   �� 5,      $
��Auto �6$   �� 5-      $
��Auto �6$    50      �� 51      $��1* �6'   �� 52      $��1* �6'   .+�55      �� $��0��6   $��0��6*    �� Orientation$ 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�56      �� $��Médico��6   $��Center=�6-   $��0,0,10,0q�6H   
�������6Z   �� 57      -   5         ��Name$ 	cmbMedico��57   +   $	��200��6    �� StaysOpenOnEdit . )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Seleccionar médico��59       �� DisplayMemberPath$ Medico��5:       �� SelectedValuePath$ MedicoID��5;      5=      �� 5?      $��0��6   $��1��6*   $ 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�5@      �� $��Fecha��6   $��Center=�6,   $��0,0,10,0q�6G   
�������6Y   (  "System.Windows.Controls.DatePicker  5A      -   5          Name$ dpFecha��5A   !   $	��200��62   $��0,0,10,0q�6>   5C      5E      5F      �� 5H   
   $��	0,118,0,0q�6   $	��112��6(   $	��Top=�65   $��Right=�6M   $	��155��6i   $��1��6u   .+�5I      �� -   5         ��Name$ btnCancelar��5I      $��Cancelar��6*   ���#395886
�������$	��130��6f   $��10q�6r   6~   �� 5J      -   5        $ btnVerAgenda��5J      $��
Ver Agenda��6+   ���#395886
�������$	��130��6i   $��10q�6u   6�   5K      �� 5M   
   $��Agenda de Citas��6   $��
0,230,10,0q�6,   $��15q�6@   $��2��6M   ��5N      �� 5O      .R�5P      �� 
�������$��16(�6D   $
��Bold"�6R   $	 5q�6d   ��6    �� 6d   5Q      .��5T      �� $
��Auto=�6   $
��Auto=�6>   .��5U      &   System.Windows.Controls.DataGrid  -   5          Name$ dgCitas��5U         AutoGenerateColumns .     CanUserAddRows .     Columns 5V      0  *System.Windows.Controls.DataGridTextColumn  5W      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      
Visibility$ 	Collapsed=�6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *CitaID+6T     5Y      $ EspecialidadID��6-   $ 	Collapsed=�6h    6E   �� *EspecialidadID+6h     5Z      $ Especialidad��6-      
IsReadOnly .  6C   �� *NombreEspecialidad+6j     5\      $ MedicoID��6-   $ 	Collapsed=�6\    6?   �� *
MedicoID+6\     5]      $ Médico��6-    .  6=   �� *Medico+6X     5_      $ 
PacienteID��6-   $ 	Collapsed=�6`    6A   �� *
PacienteID+6`     5a      $ Fecha��6-    .  6<   �� *	FechaCita+ �� StringFormat$ {0:dd/MM/yyyy}��6{     5b      $
 Hora��6-    .  6;   �� *Hora+6T     5c      $ 	Duración��6-    .  6?   �� *
Duracion+6\     5e      $ ConsultorioID��6-   $ 	Collapsed=�6f    6D   �� *ConsultorioID+6f     5f      $ Consultorio��6-    .  6B   �� *NombreConsultorio+6h     5h      $ Motivo��6-    .  6=   �� *
MotivoCita+6\     5j      $ EstadoCitaID��6-   $ 	Collapsed=�6d    6C   �� *EstadoCitaID+6d     5k      $ Estado��6-    . $ 	Collapsed=�6n    6=   �� *
EstadoCita+6n     5m      $ Comentarios��6-    . $ 	Collapsed=�6t    6B   �� *Comentarios+6t   5o      5p      5r      � 5s   
   .+�5t      �� -   5        $ btnHome��5t      $��50��6&   
�������$��974,9,-906,691q�6E   
�������.��5u      �� $��Assets/regresar.png��6   $��50��65   $��60��6A   5v      5w      5x      5y      