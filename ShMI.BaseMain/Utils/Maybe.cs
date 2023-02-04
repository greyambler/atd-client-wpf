using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ShMI.BaseMain
{
    public static class Maybe
    {
        public static TResult With<TInput, TResult>( this TInput o, Func<TInput, TResult> evaluator )
            where TInput : class
            where TResult : class
        {
            return o == null ? null : evaluator(o);
        }
        public static TResult Return<TInput, TResult>( this TInput o, Func<TInput, TResult> evaluator, TResult failurValue )
            where TInput : class
        {
            return o == null ? failurValue : evaluator(o);
        }
        public static bool ReturnSuccess<TInput>( this TInput o )
           where TInput : class
        {
            return o != null;
        }
        public static bool ThisNotNull<TInput>( this TInput o )
            where TInput : class
        {
            return o != null;
        }
        public static TInput If<TInput>( this TInput o, Predicate<TInput> evaluator )
           where TInput : class
        {
            return o == null ? null : evaluator(o) ? o : null;
        }
        public static TInput Do<TInput>( this TInput o, Action<TInput> action )
           where TInput : class
        {
            if(o == null)
            {
                return null;
            }

            action(o);
            return o;
        }

        public static string ShMI_mGet_MaskCard_String( this string _P )
        {
            int i = 0;
            int max = ((_P.Length - 5) > 0) ? _P.Length - 5 : _P.Length;

            string t = "";
            if(_P.ThisNotNull())
            {
                foreach(char sb in _P)
                {
                    if(sb != 0 && (i > 6 || i < max))
                    {
                        if(i < 6 || i > max)
                        {
                            t += sb.ToString();

                        }
                        else
                        {
                            t += "X";
                        }
                    }
                    i++;
                }
            }
            return t;
        }
        public static string ShMI_mGet_String( this sbyte[] _P )
        {
            string t = "";
            if(_P.ThisNotNull())
            {
                foreach(sbyte sb in _P)
                {
                    if(sb != 0)
                    {
                        t += ((char) sb).ToString();
                    }
                }
            }
            return t;
        }
        public static byte[] ShMI_mGet_mByte( this sbyte[] _P )
        {
            byte[] tt = new byte[_P.Length];

            for(int i = 0; i < _P.Length; i++)
            {
                tt[i] = (byte) _P[i];
            }
            return tt;
        }

        public static string ShMI_mGet_String_Mass( this sbyte[] _P, int _Encoding )
        {
            return Encoding.GetEncoding(_Encoding).GetString(_P.ShMI_mGet_mByte()).TrimEnd('\0');
        }
        public static string ShMI_mGet_String_Mass( this byte[] _P, int _Encoding )
        {
            return Encoding.GetEncoding(_Encoding).GetString(_P).TrimEnd('\0');
        }

        public static string ShMI_mGet_String_Mass_UniPos( this List<byte> _UniPos, int _Encoding )
        {
            byte[] T_UniPos = new byte[_UniPos.Count - 3];
            for(int r = 0; r < T_UniPos.Count(); r++)
            {
                T_UniPos[r] = _UniPos[r + 1];
            }
            return Encoding.GetEncoding(_Encoding).GetString(T_UniPos).TrimEnd('\0');
        }

        public static string ShMI_mGetStr_Mass_ASCII( this byte[] _P, int _Encoding )
        {
            List<byte> p = new List<byte>();
            foreach(byte b in _P)
            {
                if(b >= 48)
                {
                    p.Add(b);
                }
            }
            return Encoding.GetEncoding(_Encoding).GetString(p.ToArray());
        }

        public static string ShMI_mGet_String( this byte[] _P )
        {
            return Encoding.UTF8.GetString(_P);
        }

        public static long ShMI_mGet_Long( this sbyte[] _P )
        {
            return _P.ShMI_mGet_String().ShMI_mGet_Long();
        }
        public static long Get_LongSt( this byte[] _P )
        {
            return _P.ShMI_mGet_String().ShMI_mGet_Long();
        }
        public static long ShMI_mGet_Long( this string _P )
        {
            System.Globalization.CultureInfo curent = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");//"ru-RU");
            try
            {
                if(long.TryParse(_P, out long l))
                {
                    return l;
                }
            }
            catch(Exception)
            {
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = curent;
            }
            return -1;
        }
        public static int ShMI_mGet_Int( this byte[] _P )
        {
            return _P.ShMI_mGet_String().ShMI_mGet_Int();
        }
        public static int ShMI_mGet_Int( this string _P )
        {
            return int.TryParse(_P, out int l) ? l : -1;
        }
        public static int ShMI_mGet_Int( this sbyte[] _P )
        {
            return _P.ShMI_mGet_String().ShMI_mGet_Int();
        }


        public static sbyte[] ShMI_mGet_SbyteMassiv( this string _P, int Count )
        {
            //string nameFun = "Get_SbyteMassiv";
            //Add_Name_Func_Message(nameFun);

            _P = _P.Replace(",", ".");


            //_P = double.Parse(_P).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

            sbyte[] P = new sbyte[Count];
            char[] d_STR = _P.ToCharArray();
            for(int i = 0; i < Count; i++)
            {
                if(d_STR.Length > i)
                {
                    P[i] = (sbyte) d_STR[i] == 44 ? (sbyte) '.' : (sbyte) d_STR[i];
                }
                else
                {
                    break;
                }
            }

            return P;
        }
        public static byte[] ShMI_mGet_ByteMassiv( this string _P, int Count )
        {
            //string nameFun = "Get_SbyteMassiv";
            //Add_Name_Func_Message(nameFun);

            _P = _P.Replace(",", ".");


            //_P = double.Parse(_P).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

            byte[] P = new byte[Count];
            char[] d_STR = _P.ToCharArray();
            for(int i = 0; i < Count; i++)
            {
                if(d_STR.Length > i)
                {
                    P[i] = (byte) d_STR[i] == 44 ? (byte) '.' : (byte) d_STR[i];
                }
                else
                {
                    break;
                }
            }

            return P;
        }

        public static byte[] ShMI_mGet_ByteMassivLast( this string _P, int Count )
        {
            //string s = new string('0', Count - _P.Length);
            string s = _P.PadLeft(Count, '0');
            //s = s + _P;
            byte[] P = new byte[Count];
            char[] d_STR = s.ToCharArray();
            for(int i = 0; i < Count; i++)
            {
                P[i] = (byte) d_STR[i];
            }
            return P;
        }

        public static float ShMI_mGet_float( this sbyte[] _P )
        {
            return _P.ShMI_mGet_String().ShMI_mGet_float();
        }
        public static float ShMI_mGet_float( this byte[] _P )
        {
            return _P.ShMI_mGet_String().ShMI_mGet_float();
        }
        public static float ShMI_mGet_float( this string _P )
        {
            System.Globalization.CultureInfo curent = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");//"ru-RU");
            try
            {
                if(float.TryParse(_P, out float l))
                {
                    return l;
                }
            }
            catch(Exception)
            {
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = curent;
            }
            return -1;
        }
        public static float ShMI_float_Zero( this string _P )
        {
            if(!float.TryParse(_P.Replace(",", "."), out float _PP))
            {
                if(!float.TryParse(_P.Replace(".", ","), out _PP))
                {
                    _PP = 0;
                }
            }
            return _PP;
        }


        public static double ShMI_mGet_double( this sbyte[] _P )
        {
            return _P.ShMI_mGet_String().ShMI_mGet_double();
        }
        public static double ShMI_mGet_double( this byte[] _P )
        {
            return _P.ShMI_mGet_String().ShMI_mGet_double();
        }
        public static double ShMI_mGet_double( this string _P )
        {
            System.Globalization.CultureInfo curent = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");//"ru-RU");
            try
            {
                _ = _P.Replace(",", ".");
                if(double.TryParse(_P, out double l))
                {
                    return l;
                }
            }
            catch(Exception)
            {
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = curent;
            }
            return -1;
        }
        public static double ShMI_double_Zero( this string _P )
        {
            if(!double.TryParse(_P.Replace(",", "."), out double _PP))
            {
                if(!double.TryParse(_P.Replace(".", ","), out _PP))
                {
                    _PP = 0;
                }
            }
            return _PP;
        }


        public static byte[] ShMI_mGet_saleId()
        {
            DateTime td = DateTime.Now;
            int day = (int) td.DayOfWeek;

            string OUT_Bytes = day.ToString() + ((int) td.TimeOfDay.TotalSeconds).ToString();
            return OUT_Bytes.ShMI_mGet_ByteMassiv(6);
        }

        public static string ShMI_mGet_UINT_string( this string _STR_FLOAT )
        {
            string t1 = _STR_FLOAT;
            t1 = t1.Replace(",", ".");


            if(t1.Contains("."))
            {
                string[] T1 = t1.Split('.');
                if(T1[1].Length == 2)
                {
                    t1 = string.Format("{0}{1}", T1[0], T1[1]);
                }
                else if(T1[1].Length == 1)
                {
                    t1 = string.Format("{0}{1}0", T1[0], T1[1]);
                }
                else if(T1[1].Length == 0)
                {
                    t1 = string.Format("{0}00", T1[0]);
                }
                else if(T1[1].Length > 2)
                {
                    t1 = string.Format("{0}{1}", T1[0], T1[1].Substring(0, 2));
                }
            }
            else
            {
                t1 = string.Format("{0}00", t1);
            }
            return t1.TrimStart('0');

        }

        public static string ShMI_mGet_UINT_string( this float _FLOAT )
        {
            string t1 = string.Format("{0}", _FLOAT);
            return t1.ShMI_mGet_UINT_string();
            //decimal dec = (decimal)_FLOAT;
            //string _dec = string.Format("{0}", dec * 100).Replace(".", "").Replace(",", "").TrimStart('0');

            //return string.Format("{0}", _FLOAT * 100).Replace(".", "").Replace(",", "").TrimStart('0');

            // return string.Format("{0}", _FLOAT * 100).Replace(".", "").Replace(",", "").TrimStart('0');
        }
        //public static bool TestIsDeleteFile(string item, string NameFile)
        //{
        //    bool IsDelete = false;
        //    try
        //    {
        //        int count = Math.Abs(ShMI.Base.Properties.Settings.Default.CountDaySaveLogFile);
        //        if (count == 0)
        //            count = 1;

        //        int Max_MB = Math.Abs(ShMI.Base.Properties.Settings.Default.Max_MB_LogFile);
        //        if (Max_MB == 0)
        //            Max_MB = 1;

        //        FileInfo fi = new FileInfo(item);
        //        if ((int)(fi.Length / 1000000) > Max_MB)
        //        {
        //            return true;
        //        }
        //        string _date = fi.Name.Replace(NameFile, "").Substring(0, 8);
        //        int yyyy;
        //        int MM;
        //        int dd;
        //        int TO;
        //        if (int.TryParse(_date.Substring(0, 4), out TO))
        //        {
        //            yyyy = TO;
        //            if (int.TryParse(_date.Substring(4, 2), out TO))
        //            {
        //                MM = TO;
        //                if (int.TryParse(_date.Substring(6, 2), out TO))
        //                {
        //                    dd = TO;
        //                    DateTime dt = new DateTime(yyyy, MM, dd);
        //                    DateTime ETALON = DateTime.Now.AddDays(-count);
        //                    if (ETALON > dt)
        //                    {
        //                        IsDelete = true;
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception er)
        //    {
        //    }
        //    return IsDelete;
        //}


        public static long ShMI_My_Parse_long( this string s_value )
        {
            return long.TryParse(s_value, out long t) ? t : 0;
        }
        public static int ShMI_My_Parse_int( this string s_value )
        {
            return int.TryParse(s_value, out int t) ? t : 0;
        }
        public static short ShMI_My_Parse_short( this string s_value )
        {
            return short.TryParse(s_value, out short t) ? t : (short) 0;
        }
        public static decimal ShMI_My_Parse_decimal( this string s_value )
        {
            return decimal.TryParse(s_value, out decimal t) ? t : 0;
        }
        public static DateTime ShMI_My_Parse_DateTime( this string s_value )
        {
            return DateTime.TryParse(s_value, out DateTime t) ? t : new DateTime();
        }
    }
}
