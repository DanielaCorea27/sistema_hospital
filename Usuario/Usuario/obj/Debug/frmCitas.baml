   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	       Usuario.frmCitas   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Usuario     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � Title$  Gestión de Citas��5	   	   $	��750��6"   $
��1100��6/    � 
ResizeMode$ NoResize=�6<    � WindowStartupLocation$ CenterScreen=�6R    � WindowStyle$
 None=�6w   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $
��167* �6   .+�5   
   �� $��2��6   $	��101��6(   $	��Top=�65   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 0,1&�67   .��5      �  � Color$ #B1C9EF��6#    � Offset$	 0.0Q�63   � 5      $ #D5DEEF��6#   $	 1.0Q�63   5      5      �� 5   
   $��Gestión de Citas��6   $
��Left=�5      $	��Top=�5      $��40(�5      $��	10,35,0,0q�5      
���   �$��2��5       �� 5!   
   	 �� Orientation$	 
Horizontal=�6   $
��Left=�6.   $
��1000��6I   $��2��6V   $��
0,-710,0,0q�6j   .+�5"      �� -   5        $��btnPacientes��5"      $��	Pacientes��6)   ���#395886
�������6f   �� 5#      -   5        $��btnHistorialesMedicos��5#      $��Historiales Medicos��62   ���#395886
�������6y   �� 5$      -   5        $��
btnMedicos��5$      $��Medicos��6'   ���#395886
�������6b   �� 5%      -   5        $��btnConsultas��5%      $��	Consultas��6)   ���#395886
�������6f   �� 5&      -   5        $��
btnRecetas��5&      $��Recetas��6'   ���#395886
�������6b   �� 5'      -   5        $��btnCitas��5'      $��Citas��6%   ���#D5DEEF
�������6J   �� 5(      -   5        $��btnExamenes��5(      $��Examenes��6(   ���#395886
�������6d   �� 5)      -   5        $��btnReportes��5)      $��Reportes��6(   ���#395886
�������6P   �� 5*      -	   5        $��btnUsuarios��5*      $��Usuarios��6(   ���#395886
�������6d   5+      �� 5-   
   $��Detalles de la Cita��6   $��0,101,155,311q�60   $��15q�6G   $��2��6T   ��5.      �� 5/      .R�50      �� 
�������$��16(�6D   $
��Bold"�6R   
 �� Padding$
 5q�6d   ��6    �� 6d   51      .��54      �  � RowDefinitions 55      �� 56      $
��Auto �6$   �� 57      $
��Auto �6$   �� 58      $
��Auto �6$   �� 59      $
��Auto �6$    5<      �� 5=      $��1* �6'   �� 5>      $��1* �6'   .+�5A      �� $��0��6   $��0��6*   $	 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�5B      �� $��Médico��6   $��Center=�6-   $��0,0,10,0q�6H   
�������6Z   �� 5C      -
   5         ��Name$ 	cmbMedico��5C   +   $	��200��6    �� 
IsEditable .  �� IsTextSearchEnabled .  �� StaysOpenOnEdit . )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Seleccionar médico��5G       �� DisplayMemberPath$ Medico��5H       �� SelectedValuePath$ MedicoID��5I      63   5J      �� 5L      $��0��6   $��1��6*   $	 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�5M      �� $��Paciente��6   $��Center=�6/   $��0,0,10,0q�6J   
�������6\   �� 5N      -   5        $ cmbPaciente��5N   +   $	��200��6    .  .  . $ Seleccionar paciente��5R      $ Paciente��5T      $ 
PacienteID��5U      5V      �� 5Y      $��1��6   $��0��6*   $	 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�5Z      �� $��Fecha��6   $��Center=�6,   $��0,0,10,0q�6G   
�������6Y   (  "System.Windows.Controls.DatePicker  5[      -   5          Name$ dpFecha��5[   !   $	��200��62   $��0,0,10,0q�6>   6P   5]      �� 5_      $��1��6   $��1��6*   $	 
Horizontal=�6:   $��	0,0,10,56q�6S   $��2��6f   .+�5`      �� $
��Hora��6   $��Center=�6+   $��0,0,10,0q�6F   
�������6X   �� 5a      -   5        $ cmbHora��5a      $	��100��60   $��0,0,10,0q�6<   {�.  $
 Hora��5c      $
 Hora��5d      5f      5g      �� 5i      $��2��6   $��0��6*   $	 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�5j      �� $��Duracion��6   $��Center=�6/   $��0,0,10,0q�6J   
�������6\   �� 5k      -   5        $ cmbDuracion��5k      $	��100��64   $��0,0,10,0q�6@   .@�5m      �� $��30 min��6'   
 � Tag$ 30��68   �� 5n      $��60 min��6'   $ 60��68   5p      5q      �� 5s      $��2��6   $��1��6*   $	 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�5t      �� $��Motivo��6   $��Center=�6-   $��0,0,10,0q�6H   
�������6Z   �� 5u      -   5         ��Name$ 	txtMotivo��5u      $	��200��5v      $ Motivo de la cita��5w       %  !MaterialDesignFloatingHintTextBox#��[  5x      $��46��6N   5z      �� 5|      $��3��6   $��0��6*   $	 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�5}      �� $��Consultorio��6   $��Center=�62   $��0,0,10,0q�6M   
�������6_   �� 5~      -   5        $ cmbConsultorio��5~      $	��200��5      $ Consultorio��5�      $ NombreConsultorio��5�      $ ConsultorioID��5�      5�      �� 5�      $��3��6   $��1��6*   $	 
Horizontal=�6:   $��	0,0,10,10q�6S   .+�5�      �� $��Comentarios��6   $��Center=�62   $��0,0,10,0q�6M   
�������6_   �� 5�      -   5        $ txtComentarios��5�      $��45��5�      $	��200��5�      $ Comentarios��5�      #��[  5�       �� AcceptsReturn . 6N   5�      �� 5�      $��3��6   $��2��6*   $	 
Horizontal=�6>   $��0,45,10,-39q�6W   .+�5�      �� $��Estado��6   $��Center=�6-   $��0,0,10,0q�6H   
�������6Z   �� 5�      -   5        $ cmbEstadoCita��5�      $	��100��66   $��0,0,10,0q�6B   {�.  $ 
EstadoCita��5�      $ EstadoCitaID��5�      5�      5�      �� 5�      $��3��6   $��1��6*   $	 
Horizontal=�6:   $��0,27,10,-65q�6S   .+�5�      �� $��Especialidad��6   $��Center=�63   $��0,0,10,0q�6N   
�������6`   �� 5�      -   5        $ cmbEspecialidad��5�       .   . $ Seleccionar especialidad��5�      $ NombreEspecialidad��5�      $ EspecialidadID��5�      5�      5�      5�      �� 5�   
   $��	0,162,0,0q�6   $	��260��6(   $	��Top=�65   $��Right=�6M   $	��155��6i   $��1��6u   .+�5�      �� -   5         ��Name$ btnProgramar��5�      $��Guardar��6+   ���#395886
�������$	��130��6f   6r   �� 5�      -   5        $ btnCancelar��5�      $��Cancelar��6*   ���#395886
�������$	��130��6f   $��10q�6r   6~   �� 5�      -   5        $ btnReprogramar��5�      $��
Actualizar��6-   ���#395886
�������$	��133��6k   $��10q�6w   6�   �� 5�      -   5        $ btnEliminar��5�      $��Eliminar��6*   ���#395886
�������$	��133��6f   $��10q�6r   6~   �� 5�      -   5        $ btnVisualizar��5�      $��
Ver Agenda��6,   ���#395886
�������$	��130��6j   $��10q�6v   6�   5�      �� 5�   
   $��Agenda de Citas��6   $��
0,439,10,0q�6,   $��15q�6@   $��2��6M   ��5�      �� 5�      .R�5�      �� 
�������$��16(�6D   $
��Bold"�6R   $
 5q�6d   ��6    �� 6d   5�      .��5�      �� $
��Auto=�6   $
��Auto=�6>   .��5�      &   System.Windows.Controls.DataGrid  -   5          Name$ dgCitas��5�         AutoGenerateColumns .     CanUserAddRows .     Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6-      
Visibility$ 	Collapsed=�6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *CitaID+6T     5�      $ EspecialidadID��6-   $ 	Collapsed=�6h    6E   �� *EspecialidadID+6h     5�      $ Especialidad��6-      
IsReadOnly .  6C   �� *NombreEspecialidad+6j     5�      $ MedicoID��6-   $ 	Collapsed=�6\    6?   �� *
MedicoID+6\     5�      $ Médico��6-    .  6=   �� *Medico+6X     5�      $ 
PacienteID��6-   $ 	Collapsed=�6`    6A   �� *
PacienteID+6`     5�      $ Paciente��6-    .  6?   �� *
Paciente+6\     5�      $ Fecha��6-    .  6<   �� *	FechaCita+  �� StringFormat$  {0:dd/MM/yyyy}��6{     5�      $
 Hora��6-    .  6;   �� *Hora+6T     5�      $ 	Duración��6-    .  6?   �� *
Duracion+6\     5�      $ ConsultorioID��6-   $ 	Collapsed=�6f    6D   �� *ConsultorioID+6f     5�      $ Consultorio��6-    .  6B   �� *NombreConsultorio+6h     5�      $ Motivo��6-    .  6=   �� *
MotivoCita+6\     5�      $ EstadoCitaID��6-   $ 	Collapsed=�6d    6C   �� *EstadoCitaID+6d     5�      $ Estado��6-    .  6=   �� *
EstadoCita+6\     5�      $ Comentarios��6-    .  6B   �� *Comentarios+6b   5�      5�      5�      � 5�   
   .+�5�      �� -   5        $ btnHome��5�      $��50��6&   
�������$��974,9,-906,691q�6E   
�������.��5�      �� $��Assets/regresar.png��6   $��50��65   $��60��6A   5�      5�      5�      5�      