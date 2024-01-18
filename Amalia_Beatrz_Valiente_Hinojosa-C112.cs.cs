using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MatCom.Examen;

public class NavesEspaciales
{
    public static int MaximoRescate(bool[,] campoBatalla)
    {
        int limiteFila = campoBatalla.GetLength(0);
        int limiteColumna = campoBatalla.GetLength(1);
        int mayorRescate = 0;

        void RescatarNaves(int navesSalvadas, int fila, int columna, bool[,] campo)
        {
            if (fila >= limiteFila || fila < 0 || columna >= limiteColumna || columna < 0)
            {
                //llegue a una orilla del campo, debo comparar con el mejor rescate
                mayorRescate = Math.Max(navesSalvadas, mayorRescate);
                return;
            }

            //posibles movimientos
            int movimientos = 1;

            //- derecha:
            if (PosibleDerecha(fila, columna))
            {
                int i = columna + 1;
                while (i < limiteColumna && !campo[fila, i])
                {
                    i++;
                }
                if (campo[fila, i]) //hay una nave, la salvo y me puedo mover.
                {
                    if (EsValido(fila, i + 1) && !campo[fila, i + 1])
                    {
                        campo[fila, i] = false;
                        navesSalvadas++;
                        while (columna + movimientos < limiteColumna && !campo[fila, columna + movimientos])
                        {
                            RescatarNaves(navesSalvadas, fila, columna + movimientos, campo);
                            movimientos++;
                        }
                        campo[fila, i] = true;
                        navesSalvadas -= 1;
                    }
                    else mejorRescate = Math.Max(mejorRescate, navesSalvadas);


                }
            }

            if (PosibleArriba(fila, columna)) //arriba
            {
                movimientos = 1;
                int i = fila - 1;
                while (i >= 0 && !campo[fila, i])
                {
                    i--;
                }

                if (EsValido(i, columna) && !campo[i - 1, columna])
                {
                    campo[i, columna] = false;
                    navesSalvadas++;
                    while (fila - movimientos >= 0 && !campo[i - movimientos, columna])
                    {
                        RescatarNaves(navesSalvadas, fila - movimientos, columna, campo);
                        movimientos++;
                    }
                    campo[fila, i] = true;
                    navesSalvadas -= 1;
                }
                else mejorRescate = Math.Max(mejorRescate, navesSalvadas);
            }
            if (PosibleIzquierda(fila, columna)) //izquierda
            {
                movimientos = 1;
                int i = columna - 1;
                while (i >= 0 && !campo[fila, i])
                {
                    i--;
                }
                if (campo[fila, i]) //hay una nave, la salvo y me puedo mover.
                {
                    if (EsValido(fila, i - 1) && !campo[fila, i - 1])
                    {
                        campo[fila, i] = false;
                        navesSalvadas++;
                        while (columna - movimientos >= 0 && !campo[fila, columna - movimientos])
                        {
                            RescatarNaves(navesSalvadas, fila, columna - movimientos, campo);
                            movimientos++;
                        }
                        campo[fila, i] = true;
                        navesSalvadas -= 1;
                    }

                }
                else mejorRescate = Math.Max(mejorRescate, navesSalvadas);
            }
            //abajo
            if (PosibleAbajo(fila, columna))
            {
                movimientos = 1;
                int i = fila + 1;
                while (i < limiteFila && !campo[fila, i])
                {
                    i++;
                }

                if (EsValido(i, columna) && !campo[i + 1, columna])
                {
                    campo[i, columna] = false;
                    navesSalvadas++;
                    while (fila + movimientos < limiteFila && !campo[i + movimientos, columna])
                    {
                        RescatarNaves(navesSalvadas, fila + movimientos, columna, campo);
                        movimientos++;
                    }
                    campo[fila, i] = true;
                    navesSalvadas -= 1;
                }
                else mejorRescate = Math.Max(mejorRescate, navesSalvadas);


            }





            bool EsValido(int fila, int columna)
            {
                if (!(fila >= limiteFila || fila < 0 || columna >= limiteColumna || columna < 0))
                {
                    return true;
                }
                return false;
            }

            bool PosibleArriba(int fila, int columna)
            {
                for (int i = fila; i >= 0; i -= 1)
                {
                    if (campo[i, columna])
                    {
                        return true;
                    }
                }
                return false;
            }

            bool PosibleAbajo(int fila, int columna)
            {
                for (int i = fila; i < limiteFila; i++)
                {
                    if (campo[i, columna])
                    {
                        return true;
                    }
                }
                return false;
            }

            bool PosibleIzquierda(int fila, int columna)
            {
                for (int i = columna; i >= 0; i -= 1)
                {
                    if (campo[fila, i])
                    {
                        return true;
                    }
                }
                return false;
            }

            bool PosibleDerecha(int fila, int columna)
            {
                for (int i = columna; i < limiteColumna; i++)
                {
                    if (campo[fila, i])
                    {
                        return true;
                    }
                }
                return false;
            }

        }
        
        for (int i = 0; i < limiteColumna; i++)
        {
            for (int j = 0; j < limiteFila; j++)
            {
                if (campoBatalla[j, i])
                {
                    campoBatalla[j, i] = false;
                    RescatarNaves(0, j, i, campoBatalla);
                    campoBatalla[j, i] = true;
                }
            }
        }

        return mayorRescate;
    }
}
