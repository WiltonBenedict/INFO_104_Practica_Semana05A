using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace INFO_104_Semana05_PracticaA
{
    //INFO-104
    //Practica de Examen 02
    /*
    Crear un sistema de factura con maximo de 15 datos en el arreglo para llevar el control del peaje 
    de una autopista.
     */
    internal class Program
    {
        //Comentarios importantes
        //static clsFacturas[] factura = new clsFacturas[15];
        //factura[1] = new clsFacturas {  numeroFactura = "",  numeroPlaca = "",  numeroCaseta = 0,  fecha = "",  hora = "",  tipoVehiculo = 1,  precioPaga = 0,  precioFinal = 0,  precioVuelto = 0 };

        static clsFacturas[] factura = new clsFacturas[15];
        static bool estadoMain = true;
        static bool controlVector = false;
        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            do
            {
                Console.WriteLine("--Menu Principal--");
                Console.WriteLine("--Control de Peaje--");
                Console.WriteLine("1. Inicializar Vectores.");
                Console.WriteLine("2. Ingresar Paso Vehicular.");
                Console.WriteLine("3. Consulta de vehículos por Número de Placa.");
                Console.WriteLine("4. Modificar Datos Vehículos por Número de Placa.");
                Console.WriteLine("5. Reporte Todos los Datos de los vectores");
                Console.WriteLine("6. Salida");
                Console.WriteLine("Ingrese seleccion: ");
                int.TryParse(Console.ReadLine(), out int opc);
                Seleccion(opc);

            } while (estadoMain);
        }

        static void Seleccion(int opc)
        {
            switch(opc)
            {
                case 1:
                    Console.WriteLine("Iniciando Vectores...");
                    InicioVectores();
                    break;
                case 2:
                    Console.WriteLine("Iniciando ingresar valores...");
                    ingresoPaso();
                    break;
                case 3:
                    Console.WriteLine("Iniciando consulta...");
                    Consulta();
                    break;
                case 4:
                    Console.WriteLine("Iniciando modificacion de datos...");
                    Modificacion();
                    break;
                case 5:
                    Console.WriteLine("Iniciando reporte de datos...");
                    ReporteDatos();
                    break;
                case 6:
                    Console.WriteLine("Finalizando programa...");
                    estadoMain = false;
                    break;
                default:
                    Console.WriteLine("Dato ingresado invalido...");
                    break;
            }
        }

        //Metodos
        static void InicioVectores()
        {
            if(controlVector == false)
            {
                controlVector = true;
                Console.WriteLine("Incializando vectores...");
                //Convertir en metodo
                reestablecerVectores();
                Console.WriteLine("Vectores inicializados");
            }
            else
            {
                Console.WriteLine("Vectores ya han sido inicializados.\n ¿Desea reestablecer vectores? (1. Si. Otro numero para no)");
                int.TryParse(Console.ReadLine(), out int opc);
                if( opc == 1 ) 
                {
                    reestablecerVectores();
                    Console.WriteLine("Vectores reestablecidos");
                }
            }
        }

        static void reestablecerVectores()
        {
            for (int i = 0; i < 3; i++)
            {
                factura[i] = new clsFacturas { numeroFactura = "", numeroPlaca = "", numeroCaseta = 0, fecha = "", hora = "", tipoVehiculo = 1, precioPaga = 0, precioFinal = 0, precioVuelto = 0 };
            }
        }

        static void ingresoPaso()
        {
            if(controlVector == false) { Console.WriteLine("Vectores necesitan iniciarse. Redirigiendo..."); InicioVectores(); }
            int control = 1;
            Console.WriteLine("--Ingreso de Paso Vehicular--");
            for(int i = 0;i < 3; i++)
            {
                Console.WriteLine($"Datos de la factura {control}");
                Console.WriteLine("1. Ingrese numero de factura: ");
                string temp1 = Console.ReadLine();
                Console.WriteLine("2. Ingrese numero de placa: ");
                string temp2 = Console.ReadLine();
                Console.WriteLine("3. Ingrese numero de caseta (1,2,3): ");
                int.TryParse(Console.ReadLine(), out int temp3);
                //
                ValidarCaseta(temp3);
                ///Validacion 01
                Console.WriteLine("4. Ingrese fecha (yyyy-MM-dd): ");
                //
                string temp4 = ValidarFecha();
                //Validacion Fecha
                Console.WriteLine("5. Ingrese hora (hh:mm AM o PM): ");
                //
                string temp5 = ValidarHora();
                //Validacion Hora
                Console.WriteLine("6. Ingrese tipo de vehiculo:\n1. Motocicleta\n2.Vehículo Liviano\n3. Camión o Pesado\n4.Autobús\n ");
                int.TryParse(Console.ReadLine(), out int temp6);
                temp6 = ValidarTipoVehiculo(temp6);
                //Validacion 02
                Console.WriteLine("7. Ingrese el monto del pago");
                //
                //int.TryParse(Console.ReadLine(), out int temp7);
                //Se necesita primero el costo para validar el pago
                int temp8 = ValidarMontoPagar(temp6);
                int temp7 = ValidarPago(temp8);
                //Validacion 03 NOTA PROBLEMA
                //Precio Final (monto a pagar) (temp8) y Precio Vuelto se calculan al final (temp9) de validar los datos
                int temp9 = ValidarVuelto(temp7,temp8);//se necesita temp8 y temp7
                Console.WriteLine("Guardando cambios....");
                factura[i] = new clsFacturas { numeroFactura = temp1, numeroPlaca = temp2, numeroCaseta = temp3, fecha = temp4, hora = temp5, tipoVehiculo = temp6, precioPaga = temp7, precioFinal = temp8, precioVuelto = temp9 };
                //
                control++;
            }
        }

        static void ReporteDatos()
        {

            int cantDinero = 0;
            int cantVehiculo = 0;
            Console.WriteLine("--Imprimiendo Datos--");
            for(int i = 0; i < factura.Length; i++)
            {
                Console.WriteLine("-------");
                Console.WriteLine($"Factura: {i+1}");
                Console.WriteLine($"# Factura: {factura[i].numeroFactura}");
                Console.WriteLine($"# Placa: {factura[i].numeroPlaca}");
                Console.WriteLine($"Tipo de Vehiculo: {factura[i].tipoVehiculo}");//usar switch
                Console.WriteLine($"# Caseta: {factura[i].numeroCaseta}");
                Console.WriteLine($"Monto a pagar: {factura[i].precioFinal} colones");
                Console.WriteLine($"Paga con: {factura[i].precioPaga} colones");
                Console.WriteLine($"Vuelto: {factura[i].precioVuelto} colones");
                Console.WriteLine($"Fecha: {factura[i].fecha}");
                Console.WriteLine($"Hora: {factura[i].hora}");
                Console.WriteLine("-------");
                cantVehiculo++;
                cantDinero = cantDinero + factura[i].precioFinal;
            }
            Console.WriteLine($"Cantidad de Vehiculos: {cantVehiculo}");
            Console.WriteLine($"Total dinero recaudado: {cantDinero}");
            Console.WriteLine("--Fin del Reporte--");
            Console.WriteLine("<<<Pulse tecla para regresar >>>");
            Console.ReadLine();

        }

        static void Consulta()
        {
            if (controlVector == false) { Console.WriteLine("Vectores necesitan iniciarse. Redirigiendo..."); InicioVectores(); }
            Console.WriteLine("--Consulta por Placa--");
            bool estadoConsulta = true;
            while(estadoConsulta)
            {
                Console.WriteLine("Ingrese la placa a buscar: ");
                string busq = Console.ReadLine();
                int indice = Busqueda(busq);
                if (indice != -1)
                {
                    Console.WriteLine("Reporte encontrado");
                    Console.WriteLine("-------------");
                    Console.WriteLine($"# Factura: {factura[indice].numeroFactura}");
                    Console.WriteLine($"# Placa: {factura[indice].numeroPlaca}");
                    Console.WriteLine($"Tipo de Vehiculo: {factura[indice].tipoVehiculo}");//usar switch
                    Console.WriteLine($"# Caseta: {factura[indice].numeroCaseta}");
                    Console.WriteLine($"Monto a pagar: {factura[indice].precioFinal} colones");
                    Console.WriteLine($"Paga con: {factura[indice].precioPaga} colones");
                    Console.WriteLine($"Vuelto: {factura[indice].precioVuelto} colones");
                    Console.WriteLine($"Fecha: {factura[indice].fecha}");
                    Console.WriteLine($"Hora: {factura[indice].hora}");
                    Console.WriteLine("-------------");
                    Console.WriteLine("¿Salir al menu principal? (1. Si) ");
                    int.TryParse(Console.ReadLine(), out int opc);
                    if (opc == 1) { estadoConsulta = false; }
                }
                else
                {
                    Console.WriteLine("Reporte no encontrado");
                    Console.WriteLine("¿Salir al menu principal? (1. Si) ");
                    int.TryParse(Console.ReadLine(), out int opc);
                    if(opc == 1) {estadoConsulta =false;}
                }
            }
        }

        static void Modificacion()
        {
            if (controlVector == false) { Console.WriteLine("Vectores necesitan iniciarse. Redirigiendo..."); InicioVectores(); }
            Console.WriteLine("--Modificacion de Reportes--");
            bool estado = true;
            while(estado)
            {
                Console.WriteLine("Ingrese la placa relacionada al reporte a modificar: ");
                string busq = Console.ReadLine();
                int indice = Busqueda(busq);
                if(indice != -1)
                {
                    Console.WriteLine("Reporte encontrado");
                    Console.WriteLine("-------------");
                    Console.WriteLine($"# Factura: {factura[indice].numeroFactura}");
                    Console.WriteLine($"# Placa: {factura[indice].numeroPlaca}");
                    Console.WriteLine($"Tipo de Vehiculo: {factura[indice].tipoVehiculo}");//usar switch
                    Console.WriteLine($"# Caseta: {factura[indice].numeroCaseta}");
                    Console.WriteLine($"Monto a pagar: {factura[indice].precioFinal} colones");
                    Console.WriteLine($"Paga con: {factura[indice].precioPaga} colones");
                    Console.WriteLine($"Vuelto: {factura[indice].precioVuelto} colones");
                    Console.WriteLine($"Fecha: {factura[indice].fecha}");
                    Console.WriteLine($"Hora: {factura[indice].hora}");
                    Console.WriteLine("-------------");
                    Console.WriteLine("Seleccion el dato a modificar: ");
                    Console.WriteLine("1. # Factura.\n2.# Placa.\n3.Tipo de Vehiculo\n4");
                    int.TryParse(Console.ReadLine(), out int opc);
                    Cambios(opc,indice);
                }
                else
                {
                    Console.WriteLine("Reporte no encontrado");
                    Console.WriteLine("¿Salir al menu principal? (1. Si) ");
                    int.TryParse(Console.ReadLine(), out int opc);
                    if (opc == 1) { estado = false; }
                }
            }
        }
        static void Cambios(int opc,int indice)
        {
            string temp = "";
            switch (opc)
            {
                case 1:
                    Console.WriteLine("Ingrese el nuevo # de Factura: ");
                    temp = Console.ReadLine();
                    factura[indice].numeroFactura = temp;
                    break;
                case 2:
                    Console.WriteLine("Ingrese el nuevo # de Placa: ");
                    temp = Console.ReadLine();
                    factura[indice].numeroFactura = temp;
                    break;
                case 3:
                    Console.WriteLine("Ingrese el nuevo tipo de vehiculo:\n1. Motocicleta\n2.Vehículo Liviano\n3. Camión o Pesado\n4.Autobús\n ");
                    int.TryParse(Console.ReadLine(), out int tempTipo);
                    tempTipo = ValidarTipoVehiculo(tempTipo);
                    ValidarCambio(tempTipo,indice);

                    break;
                default:
                    Console.WriteLine("Valor ingresado invalido");
                    break;
            }
        }

        //Funciones
        static int ValidarMontoPagar(int tipoVehiculo)
        {
            switch (tipoVehiculo)
            {
                case 1:
                    return 500;
                case 2:
                    return 700;
                case 3:
                    return 2700;
                case 4:
                    return 3700;
                default:
                    return 0;
            }
        }

        static int ValidarVuelto(int pago, int costo)
        {
            int vuelto = pago - costo;
            //bool estadoValidar = false;
            //
            /*No necesario ya que se valido previamente
            if(vuelto < 0) 
            {
                estadoValidar = true;
                while (estadoValidar)
                {
                    Console.WriteLine("Error. El pago ingresado no es suficiente para pagar el peaje");
                    Console.WriteLine("Ingrese de nuevo el pago:");
                    int.TryParse(Console.ReadLine(), out pago);
                    vuelto = pago - costo;
                    if (vuelto>0) { estadoValidar = false; Console.WriteLine("Validacion superada"); }
                }
            }
            */
            return vuelto;
        }

        static int ValidarPago(int costo)
        {
            int pago = 0;
            bool estadoValidar = true;
            while (estadoValidar)
            {
                int.TryParse(Console.ReadLine(), out  pago);
                if(pago-costo < 0)
                {
                    Console.WriteLine("Error. El pago ingresado no es suficiente para pagar el peaje");
                    Console.WriteLine("Ingrese de nuevo el pago:");
                }
                else
                {
                    estadoValidar = false;
                }

            }
            return pago;
        }

        static int ValidarCaseta(int caseta)
        {
            bool estadoValidar = false;
            if ( caseta < 1 || caseta > 3 ) 
            {
                estadoValidar = true;
                while(estadoValidar)
                {
                    Console.WriteLine("Error. Numero de caseta invalido");
                    Console.WriteLine("Ingrese el numero de caseta nuevamente (1,2,3): ");
                    int.TryParse(Console.ReadLine(), out caseta);
                    if (caseta >= 1 || caseta <= 3) { estadoValidar=false; Console.WriteLine("Validacion superada"); }
                }   
            }
            return caseta;
        }
    
        static string ValidarFecha()
        {
            /*
            DateTime.TryParseExact(userInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime date)
             */
            string fecha = "";
            bool estadoValidar = true;
            while (estadoValidar)
            {
                //DateTime born = DateTime.Parse(Console.ReadLine());
                //DateTime fecha = DateTime.Parse((Console.ReadLine()));
                fecha = Console.ReadLine();
                //DateTime.TryParse(userInput, out DateTime date)
                if (DateTime.TryParse(fecha, out DateTime validacion))
                {
                    //Opcion de visibilidad
                    Console.WriteLine($"Hora Valida.\nString: {fecha}\nDateTime: {validacion: yyyy-MM-dd}");
                    estadoValidar = false;
                }
                else
                {
                    Console.WriteLine("Formato de fecha incorrecto. Intente de nuevo.\n Formato correcto:  (yyyy-MM-dd)");
                }
            }
            return fecha;
        }

        static string ValidarHora()
        {
            string hora = "";
            bool estadoValidar = true;
            while (estadoValidar)
            {
                hora = Console.ReadLine();
                //if (DateTime.TryParseExact(userInput, "h:mm tt", null, System.Globalization.DateTimeStyles.None, out DateTime time))
                if(DateTime.TryParseExact(hora,"h:mm tt",null, System.Globalization.DateTimeStyles.None, out DateTime validacion))
                {   
                    //Opcion de visibilidad
                    Console.WriteLine($"Hora Valida.\nString: {hora}\nDateTime: {validacion: hh:mm tt}");
                    estadoValidar = false;
                }
                else
                {
                    Console.WriteLine("Formato de hora incorrecto. Intente de nuevo.\n Formato correcto:  (hh:mm AM o PM)");
                }
            }
            return hora;
        }
    
        static int ValidarTipoVehiculo(int tipo)
        {
            bool estadoValidar = false;
            if (tipo < 1 || tipo > 4)
            {
                estadoValidar = true;
                while (estadoValidar)
                {
                    Console.WriteLine("Error. Numero de vehiculo invalido");
                    Console.WriteLine("Ingrese tipo de vehiculo\n1. Motocicleta\n2.Vehículo Liviano\n3. Camión o Pesado\n4.Autobús\n: ");
                    int.TryParse(Console.ReadLine(), out tipo);
                    if (tipo >= 1 || tipo <= 4) { estadoValidar = false; Console.WriteLine("Validacion superada"); }
                }
            }
            return tipo;
        }

        static int Busqueda(string busq)
        {
            int retVal = -1;
            for (int i = 0; i < factura.Length; i++)
            {
                if(busq == factura[i].numeroPlaca)
                {
                    retVal = i; break;
                }
            }
            return retVal;
        }
        
        //Validacion pero no funcion
        static void ValidarCambio(int tipo, int indice)
        {
            int nuevoCosto = ValidarMontoPagar(tipo);
            if (factura[indice].precioPaga-nuevoCosto < 0)
            {
                Console.WriteLine("Pago original insuficiente");
                Console.WriteLine("¿Adjustar pago original? (1.Si. Otro numero para no");
                int.TryParse(Console.ReadLine(), out int opc);
                if(opc == 1)
                {
                    bool estado = true;
                    while(estado)
                    {
                        Console.WriteLine($"Pago pendiente: {nuevoCosto}\n Previo pago: {factura[indice].precioPaga}");
                        Console.WriteLine("Ingrese el nuevo pago: ");
                        int.TryParse(Console.ReadLine(), out int nuevoPago);
                        if(nuevoPago - nuevoCosto < 0)
                        {
                            Console.WriteLine("Pago insuficiente. Intente de nuevo");
                            Console.WriteLine("¿Intentar de nuevo? 1. Para terminar. Otro numero para intentar de nuevo");
                            int.TryParse(Console.ReadLine(), out int opc2);
                            if(opc2 == 1)
                            {
                                estado = false;
                            }
                        }
                        else
                        {
                            estado = false;
                            Console.WriteLine("Realizando cambios...");
                            factura[indice].precioFinal = nuevoCosto;
                            factura[indice].precioPaga = nuevoPago;
                            factura[indice].precioVuelto = factura[indice].precioPaga - factura[indice].precioFinal;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Redirigiendo...");
                }
            }
            else
            {
                Console.WriteLine("Realizando cambios...");
                factura[indice].precioFinal = nuevoCosto;
                factura[indice].precioVuelto  = factura[indice].precioPaga - factura[indice].precioFinal;
            }
        }
    }
}
