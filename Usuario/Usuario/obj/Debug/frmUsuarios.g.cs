﻿#pragma checksum "..\..\frmUsuarios.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7AEFABFABACF765BA333B427F2EFDA2965A7746F4CE0C48D82230020951F1489"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using RootLibrary.WPF.Localization;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Usuario;


namespace Usuario {
    
    
    /// <summary>
    /// frmUsuarios
    /// </summary>
    public partial class frmUsuarios : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNombre;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtApellido;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEmail;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbRoles;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtClave;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtConfirmacion;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBuscar;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgUsuarios;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPacientes;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHistorialesMedicos;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMedicos;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConsultas;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRecetas;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCitas;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExamenes;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReportes;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUsuarios;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNewUser;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGuardar;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditar;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEliminar;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelar;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\frmUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHome;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Usuario;component/frmusuarios.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmUsuarios.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\frmUsuarios.xaml"
            ((Usuario.frmUsuarios)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtNombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtApellido = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cmbRoles = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.txtClave = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 7:
            this.txtConfirmacion = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 8:
            this.txtBuscar = ((System.Windows.Controls.TextBox)(target));
            
            #line 47 "..\..\frmUsuarios.xaml"
            this.txtBuscar.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtBuscar_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.dgUsuarios = ((System.Windows.Controls.DataGrid)(target));
            
            #line 52 "..\..\frmUsuarios.xaml"
            this.dgUsuarios.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgUsuarios_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnPacientes = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\frmUsuarios.xaml"
            this.btnPacientes.Click += new System.Windows.RoutedEventHandler(this.btnPacientes_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnHistorialesMedicos = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\frmUsuarios.xaml"
            this.btnHistorialesMedicos.Click += new System.Windows.RoutedEventHandler(this.btnHistorialesMedicos_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnMedicos = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\frmUsuarios.xaml"
            this.btnMedicos.Click += new System.Windows.RoutedEventHandler(this.btnMedicos_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.btnConsultas = ((System.Windows.Controls.Button)(target));
            
            #line 80 "..\..\frmUsuarios.xaml"
            this.btnConsultas.Click += new System.Windows.RoutedEventHandler(this.btnConsultas_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.btnRecetas = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\frmUsuarios.xaml"
            this.btnRecetas.Click += new System.Windows.RoutedEventHandler(this.btnRecetas_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.btnCitas = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\frmUsuarios.xaml"
            this.btnCitas.Click += new System.Windows.RoutedEventHandler(this.btnCitas_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.btnExamenes = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\frmUsuarios.xaml"
            this.btnExamenes.Click += new System.Windows.RoutedEventHandler(this.btnExamenes_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.btnReportes = ((System.Windows.Controls.Button)(target));
            return;
            case 18:
            this.btnUsuarios = ((System.Windows.Controls.Button)(target));
            return;
            case 19:
            this.btnNewUser = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\frmUsuarios.xaml"
            this.btnNewUser.Click += new System.Windows.RoutedEventHandler(this.btnNewUser_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            this.btnGuardar = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\frmUsuarios.xaml"
            this.btnGuardar.Click += new System.Windows.RoutedEventHandler(this.btnGuardar_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            this.btnEditar = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\frmUsuarios.xaml"
            this.btnEditar.Click += new System.Windows.RoutedEventHandler(this.btnEditar_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            this.btnEliminar = ((System.Windows.Controls.Button)(target));
            
            #line 104 "..\..\frmUsuarios.xaml"
            this.btnEliminar.Click += new System.Windows.RoutedEventHandler(this.btnEliminar_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            this.btnCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 107 "..\..\frmUsuarios.xaml"
            this.btnCancelar.Click += new System.Windows.RoutedEventHandler(this.btnCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 24:
            this.btnHome = ((System.Windows.Controls.Button)(target));
            
            #line 113 "..\..\frmUsuarios.xaml"
            this.btnHome.Click += new System.Windows.RoutedEventHandler(this.btnHome_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

