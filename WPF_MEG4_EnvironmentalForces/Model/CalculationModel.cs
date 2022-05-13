using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MathInterpolations;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace WPF_MEG4_EnvironmentalForces
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum VesselType
    {

        Tanker,
        [Description("Gas Carrier")]
        GasCarrier
    }

    public enum LoadingCondition
    {
        NA,
        Loaded,
        Ballasted,
    }

    public enum BowShape
    {

        NA,
        Cylindrical,
        Bulbous
    }

    public enum TankShape
    {
        NA,
        Prismatic,
        Spherical
    }

    public enum Coefficient
    {
        CXw,
        CYw,
        CXYw,
        CXc,
        CYc,
        CXYc
    }

    public class CoefficientCalculator
    {
        public VesselType VesselType;
        public BowShape BowShape;
        public LoadingCondition LoadingCondition;
        public TankShape TankShape;

        public static double GetCoefficient(Coefficient coefficient, double theta, VesselType vesselType,
                                            out List<double> X, out List<double> Y,
                                            LoadingCondition loadingCondition = LoadingCondition.NA, BowShape bowShape = BowShape.NA, TankShape tankShape = TankShape.NA,
                                            double Wd = 1, double T = 1)
        {
            string[] data = new string[3];
            double[] Xs;
            double[] Ys;

            // Check Angle.
            if (theta < 0)
            {
                theta = 0;
                MessageBox.Show("Angle set to 0deg.");
            }
            if (theta > 180 && theta <= 360) theta = theta - 180;
            if (theta > 360)
            {
                theta = 180;
                MessageBox.Show("Angle set to 180deg.");
            }

            // Selecet Coefficient Data File
            switch (coefficient)
            {
                //Wind Coefficients
                case Coefficient.CXw:
                    switch (vesselType)
                    {
                        case VesselType.Tanker:
                            switch (loadingCondition)
                            {
                                case LoadingCondition.NA:
                                    MessageBox.Show("Define Loading Condition");
                                    break;
                                case LoadingCondition.Loaded:
                                    data = File.ReadAllLines(@"CoefficientsData\CXw_Tanker_Loaded.dat", Encoding.UTF8);
                                    break;
                                case LoadingCondition.Ballasted:
                                    switch (bowShape)
                                    {
                                        case BowShape.NA:
                                            MessageBox.Show("Define BowShape");
                                            break;
                                        case BowShape.Cylindrical:
                                            data = File.ReadAllLines(@"CoefficientsData\CXw_Tanker_Ballasted_Cylindrical.dat", Encoding.UTF8);
                                            break;
                                        case BowShape.Bulbous:
                                            data = File.ReadAllLines(@"CoefficientsData\CXw_Tanker_Ballasted_Bulbous.dat", Encoding.UTF8);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case VesselType.GasCarrier:
                            switch (tankShape)
                            {
                                case TankShape.NA:
                                    MessageBox.Show("Define TankShape");
                                    break;
                                case TankShape.Prismatic:
                                    data = File.ReadAllLines(@"CoefficientsData\CXw_GasCarrier_Prismatic.dat", Encoding.UTF8);
                                    break;
                                case TankShape.Spherical:
                                    data = File.ReadAllLines(@"CoefficientsData\CXw_GasCarrier_Spherical.dat", Encoding.UTF8);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                case Coefficient.CYw:
                    switch (vesselType)
                    {
                        case VesselType.Tanker:
                            switch (loadingCondition)
                            {
                                case LoadingCondition.NA:
                                    MessageBox.Show("Define Loading Condition");
                                    break;
                                case LoadingCondition.Loaded:
                                    data = File.ReadAllLines(@"CoefficientsData\CYw_Tanker_Loaded.dat", Encoding.UTF8);
                                    break;
                                case LoadingCondition.Ballasted:
                                    data = File.ReadAllLines(@"CoefficientsData\CYw_Tanker_Ballasted.dat", Encoding.UTF8);
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case VesselType.GasCarrier:
                            switch (tankShape)
                            {
                                case TankShape.NA:
                                    MessageBox.Show("Define TankShape");
                                    break;
                                case TankShape.Prismatic:
                                    data = File.ReadAllLines(@"CoefficientsData\CYw_GasCarrier_Prismatic.dat", Encoding.UTF8);
                                    break;
                                case TankShape.Spherical:
                                    data = File.ReadAllLines(@"CoefficientsData\CYw_GasCarrier_Spherical.dat", Encoding.UTF8);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                case Coefficient.CXYw:
                    switch (vesselType)
                    {
                        case VesselType.Tanker:
                            switch (loadingCondition)
                            {
                                case LoadingCondition.NA:
                                    MessageBox.Show("Define Loading Condition");
                                    break;
                                case LoadingCondition.Loaded:
                                    data = File.ReadAllLines(@"CoefficientsData\CXYw_Tanker_Loaded.dat", Encoding.UTF8);
                                    break;
                                case LoadingCondition.Ballasted:
                                    data = File.ReadAllLines(@"CoefficientsData\CXYw_Tanker_Ballasted.dat", Encoding.UTF8);
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case VesselType.GasCarrier:
                            switch (tankShape)
                            {
                                case TankShape.NA:
                                    MessageBox.Show("Define TankShape");
                                    break;
                                case TankShape.Prismatic:
                                    data = File.ReadAllLines(@"CoefficientsData\CXYw_GasCarrier_Prismatic.dat", Encoding.UTF8);
                                    break;
                                case TankShape.Spherical:
                                    data = File.ReadAllLines(@"CoefficientsData\CXYw_GasCarrier_Spherical.dat", Encoding.UTF8);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                //Current Coefficients
                case Coefficient.CXc:
                    switch (vesselType)
                    {
                        case VesselType.Tanker:
                            switch (loadingCondition)
                            {
                                case LoadingCondition.NA:
                                    MessageBox.Show("Define Loading Condition");
                                    break;

                                case LoadingCondition.Loaded:
                                    Xs = new double[] { 1.02, 1.05, 1.1, 3.0 };
                                    Ys = new double[4];

                                    data = File.ReadAllLines(@"CoefficientsData\CXc_Tanker_Loaded_1.02.dat", Encoding.UTF8);
                                    Ys[0] = InterpolateAngle(theta, data, out List<double> X0, out List<double> Y0);

                                    data = File.ReadAllLines(@"CoefficientsData\CXc_Tanker_Loaded_1.05.dat", Encoding.UTF8);
                                    Ys[1] = InterpolateAngle(theta, data, out List<double> X1, out List<double> Y1);

                                    data = File.ReadAllLines(@"CoefficientsData\CXc_Tanker_Loaded_1.10.dat", Encoding.UTF8);
                                    Ys[2] = InterpolateAngle(theta, data, out List<double> X2, out List<double> Y2);

                                    data = File.ReadAllLines(@"CoefficientsData\CXc_Tanker_Loaded_3.00.dat", Encoding.UTF8);
                                    Ys[3] = InterpolateAngle(theta, data, out List<double> X3, out List<double> Y3);

                                    return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.02, 3.0, out X, out Y);

                                case LoadingCondition.Ballasted:
                                    Xs = new double[] { 1.05, 1.1, 3.0 };
                                    Ys = new double[3];

                                    data = File.ReadAllLines(@"CoefficientsData\CXc_Tanker_Ballasted_1.05.dat", Encoding.UTF8);
                                    Ys[0] = InterpolateAngle(theta, data, out List<double> X4, out List<double> Y4);

                                    data = File.ReadAllLines(@"CoefficientsData\CXc_Tanker_Ballasted_1.10.dat", Encoding.UTF8);
                                    Ys[1] = InterpolateAngle(theta, data, out List<double> X5, out List<double> Y5);

                                    data = File.ReadAllLines(@"CoefficientsData\CXc_Tanker_Ballasted_3.00.dat", Encoding.UTF8);
                                    Ys[2] = InterpolateAngle(theta, data, out List<double> X6, out List<double> Y6);

                                    return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.05, 3.0, out X, out Y);

                                default:
                                    break;
                            }
                            break;

                        case VesselType.GasCarrier:
                            Xs = new double[] { 1.05, 1.1, 3.0 };
                            Ys = new double[3];

                            data = File.ReadAllLines(@"CoefficientsData\CXc_GasCarrier_1.05.dat", Encoding.UTF8);
                            Ys[0] = InterpolateAngle(theta, data, out List<double> X7, out List<double> Y7);

                            data = File.ReadAllLines(@"CoefficientsData\CXc_GasCarrier_1.10.dat", Encoding.UTF8);
                            Ys[1] = InterpolateAngle(theta, data, out List<double> X8, out List<double> Y8);

                            data = File.ReadAllLines(@"CoefficientsData\CXc_GasCarrier_3.00.dat", Encoding.UTF8);
                            Ys[2] = InterpolateAngle(theta, data, out List<double> X9, out List<double> Y9);

                            return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.05, 3.0, out X, out Y);

                        default:
                            break;
                    }
                    break;

                case Coefficient.CYc:
                    switch (vesselType)
                    {
                        case VesselType.Tanker:
                            switch (loadingCondition)
                            {
                                case LoadingCondition.NA:
                                    MessageBox.Show("Define Loading Condition");
                                    break;

                                case LoadingCondition.Loaded:
                                    Xs = new double[] { 1.02, 1.05, 1.1, 3.0, 6.0 };
                                    Ys = new double[5];

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Loaded_1.02.dat", Encoding.UTF8);
                                    Ys[0] = InterpolateAngle(theta, data, out List<double> X0, out List<double> Y0);

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Loaded_1.05.dat", Encoding.UTF8);
                                    Ys[1] = InterpolateAngle(theta, data, out List<double> X1, out List<double> Y1);

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Loaded_1.10.dat", Encoding.UTF8);
                                    Ys[2] = InterpolateAngle(theta, data, out List<double> X2, out List<double> Y2);

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Loaded_3.00.dat", Encoding.UTF8);
                                    Ys[3] = InterpolateAngle(theta, data, out List<double> X3, out List<double> Y3);

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Loaded_6.00.dat", Encoding.UTF8);
                                    Ys[4] = InterpolateAngle(theta, data, out List<double> X4, out List<double> Y4);

                                    return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.02, 6.0, out X, out Y);

                                case LoadingCondition.Ballasted:
                                    Xs = new double[] { 1.05, 1.1, 3.0, 4.4 };
                                    Ys = new double[4];

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Ballasted_1.05.dat", Encoding.UTF8);
                                    Ys[0] = InterpolateAngle(theta, data, out List<double> X5, out List<double> Y5);

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Ballasted_1.10.dat", Encoding.UTF8);
                                    Ys[1] = InterpolateAngle(theta, data, out List<double> X6, out List<double> Y6);

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Ballasted_3.00.dat", Encoding.UTF8);
                                    Ys[2] = InterpolateAngle(theta, data, out List<double> X7, out List<double> Y7);

                                    data = File.ReadAllLines(@"CoefficientsData\CYc_Tanker_Ballasted_4.40.dat", Encoding.UTF8);
                                    Ys[3] = InterpolateAngle(theta, data, out List<double> X8, out List<double> Y8);

                                    return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.05, 4.4, out X, out Y);

                                default:
                                    break;
                            }
                            break;

                        case VesselType.GasCarrier:
                            Xs = new double[] { 1.05, 1.1, 3.0 };
                            Ys = new double[3];

                            data = File.ReadAllLines(@"CoefficientsData\CYc_GasCarrier_1.05.dat", Encoding.UTF8);
                            Ys[0] = InterpolateAngle(theta, data, out List<double> X9, out List<double> Y9);

                            data = File.ReadAllLines(@"CoefficientsData\CYc_GasCarrier_1.10.dat", Encoding.UTF8);
                            Ys[1] = InterpolateAngle(theta, data, out List<double> X10, out List<double> Y10);

                            data = File.ReadAllLines(@"CoefficientsData\CYc_GasCarrier_3.00.dat", Encoding.UTF8);
                            Ys[2] = InterpolateAngle(theta, data, out List<double> X11, out List<double> Y11);

                            return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.05, 3.0, out X, out Y);

                        default:
                            break;
                    }
                    break;

                case Coefficient.CXYc:
                    switch (vesselType)
                    {
                        case VesselType.Tanker:
                            switch (loadingCondition)
                            {
                                case LoadingCondition.NA:
                                    MessageBox.Show("Define Loading Condition");
                                    break;

                                case LoadingCondition.Loaded:
                                    Xs = new double[] { 1.02, 1.05, 1.1, 3.0 };
                                    Ys = new double[4];

                                    data = File.ReadAllLines(@"CoefficientsData\CXYc_Tanker_Loaded_1.02.dat", Encoding.UTF8);
                                    Ys[0] = InterpolateAngle(theta, data, out List<double> X0, out List<double> Y0);

                                    data = File.ReadAllLines(@"CoefficientsData\CXYc_Tanker_Loaded_1.05.dat", Encoding.UTF8);
                                    Ys[1] = InterpolateAngle(theta, data, out List<double> X1, out List<double> Y1);

                                    data = File.ReadAllLines(@"CoefficientsData\CXYc_Tanker_Loaded_1.10.dat", Encoding.UTF8);
                                    Ys[2] = InterpolateAngle(theta, data, out List<double> X2, out List<double> Y2);

                                    data = File.ReadAllLines(@"CoefficientsData\CXYc_Tanker_Loaded_3.00.dat", Encoding.UTF8);
                                    Ys[3] = InterpolateAngle(theta, data, out List<double> X3, out List<double> Y3);

                                    return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.02, 3.0, out X, out Y);

                                case LoadingCondition.Ballasted:
                                    Xs = new double[] { 1.05, 1.1, 3.0 };
                                    Ys = new double[3];

                                    data = File.ReadAllLines(@"CoefficientsData\CXYc_Tanker_Ballasted_1.05.dat", Encoding.UTF8);
                                    Ys[0] = InterpolateAngle(theta, data, out List<double> X4, out List<double> Y4);

                                    data = File.ReadAllLines(@"CoefficientsData\CXYc_Tanker_Ballasted_1.10.dat", Encoding.UTF8);
                                    Ys[1] = InterpolateAngle(theta, data, out List<double> X5, out List<double> Y5);

                                    data = File.ReadAllLines(@"CoefficientsData\CXYc_Tanker_Ballasted_3.00.dat", Encoding.UTF8);
                                    Ys[2] = InterpolateAngle(theta, data, out List<double> X6, out List<double> Y6);

                                    return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.05, 3.0, out X, out Y);

                                default:
                                    break;
                            }
                            break;

                        case VesselType.GasCarrier:
                            Xs = new double[] { 1.05, 1.1, 3.0 };
                            Ys = new double[3];

                            data = File.ReadAllLines(@"CoefficientsData\CXYc_GasCarrier_1.05.dat", Encoding.UTF8);
                            Ys[0] = InterpolateAngle(theta, data, out List<double> X7, out List<double> Y7);

                            data = File.ReadAllLines(@"CoefficientsData\CXYc_GasCarrier_1.10.dat", Encoding.UTF8);
                            Ys[1] = InterpolateAngle(theta, data, out List<double> X8, out List<double> Y8);

                            data = File.ReadAllLines(@"CoefficientsData\CXYc_GasCarrier_3.00.dat", Encoding.UTF8);
                            Ys[2] = InterpolateAngle(theta, data, out List<double> X9, out List<double> Y9);

                            return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.05, 3.0, out X, out Y);

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

            return InterpolateAngle(theta, data, out X, out Y);
        }


        public static double GetK(double percentage, double Wd, double T, out List<double> X, out List<double> Y)
        {
            string[] data = new string[3];
            double[] Xs;
            double[] Ys;

            Xs = new double[] { 1.05, 1.1, 1.2, 1.5, 3.0 };
            Ys = new double[5];

            data = File.ReadAllLines(@"CoefficientsData\K_1.05.dat", Encoding.UTF8);
            Ys[0] = InterpolateNumber(percentage, data, out List<double> X0, out List<double> Y0);

            data = File.ReadAllLines(@"CoefficientsData\K_1.10.dat", Encoding.UTF8);
            Ys[1] = InterpolateNumber(percentage, data, out List<double> X1, out List<double> Y1);

            data = File.ReadAllLines(@"CoefficientsData\K_1.20.dat", Encoding.UTF8);
            Ys[2] = InterpolateNumber(percentage, data, out List<double> X2, out List<double> Y2);

            data = File.ReadAllLines(@"CoefficientsData\K_1.50.dat", Encoding.UTF8);
            Ys[3] = InterpolateNumber(percentage, data, out List<double> X3, out List<double> Y3);

            data = File.ReadAllLines(@"CoefficientsData\K_3.00.dat", Encoding.UTF8);
            Ys[4] = InterpolateNumber(percentage, data, out List<double> X4, out List<double> Y5);

            return InterpolateDraftRatio(Xs, Ys, Wd, T, 1.05, 3.0, out X, out Y);

        }

        private static double InterpolateAngle(double theta, string[] data, out List<double> X, out List<double> Y)
        {
            X = new List<double>();
            Y = new List<double>();

            var o = data[0].Split(',');
            Double.TryParse(o[0], out double xOffset);
            Double.TryParse(o[1], out double yOffset);
            Double.TryParse(o[2], out double xScale);
            Double.TryParse(o[3], out double yScale);

            foreach (var line in data.Skip(1))
            {
                var l = line.Split(',');
                Double.TryParse(l[0], out double x);
                Double.TryParse(l[1], out double y);
                X.Add(180 - (xScale * (x - xOffset)));
                Y.Add(yScale * (y - yOffset));
            }
            return Interpolations.LinearInterpolation(X.ToArray(), Y.ToArray(), theta);

        }

        private static double InterpolateNumber(double theta, string[] data, out List<double> X, out List<double> Y)
        {
            X = new List<double>();
            Y = new List<double>();

            var o = data[0].Split(',');
            Double.TryParse(o[0], out double xOffset);
            Double.TryParse(o[1], out double yOffset);
            Double.TryParse(o[2], out double xScale);
            Double.TryParse(o[3], out double yScale);

            foreach (var line in data.Skip(1))
            {
                var l = line.Split(',');
                Double.TryParse(l[0], out double x);
                Double.TryParse(l[1], out double y);
                X.Add(xScale * (x - xOffset));
                Y.Add(yScale * (y - yOffset));
            }
            return Interpolations.LinearInterpolation(X.ToArray(), Y.ToArray(), theta);
        }

        private static double InterpolateDraftRatio(double[] Xs, double[] Ys, double Wd, double T, double lower, double upper, out List<double> X, out List<double> Y)
        {
            X = Xs.ToList();
            Y = Ys.ToList();
            double r = Wd / T;
            if (lower <= r && r <= upper)
                return Interpolations.LinearInterpolation(Xs, Ys, r);
            else
            {
                MessageBox.Show("Wd/T out of range");
                return -1;
            }
        }
    }

    public class ForceCalculation : Observable
    {
        public string VesselName { get; set; } = "";

        //Air density  kg/m^3
        public const double ρw = 1.28;

        //Water density kg/m^3
        public const double ρc = 1025;

        //Gravity acceleration m/sec^2
        public const double g = 9.81;

        //Vessel Type
        private VesselType _vesselType;
        public VesselType vesselType
        {
            get { return _vesselType; }
            set
            {
                if (value != _vesselType)
                {
                    _vesselType = value;
                    if (_vesselType == VesselType.Tanker)
                    {
                        bowShape = BowShape.Bulbous;
                        tankShape = TankShape.NA;
                    }
                    if (_vesselType == VesselType.GasCarrier)
                    {
                        tankShape = TankShape.Prismatic;
                        bowShape = BowShape.NA;
                    }

                    NotifyChange("");
                }
            }
        }

        //Loading Condition
        private LoadingCondition _loadingCondition;
        public LoadingCondition loadingCondition
        {
            get { return _loadingCondition; }
            set
            {
                if (value != _loadingCondition)
                {
                    _loadingCondition = value;
                    NotifyChange("");
                }
            }
        }
        //Bow Shape
        private BowShape _bowShape;
        public BowShape bowShape
        {
            get { return _bowShape; }
            set
            {
                if (value != _bowShape)
                {
                    _bowShape = value;
                    NotifyChange("");
                }
            }
        }

        //Tank Shape
        private TankShape _tankShape;
        public TankShape tankShape
        {
            get { return _tankShape; }
            set
            {
                if (value != _tankShape)
                {
                    _tankShape = value;
                    NotifyChange("");
                }
            }
        }

        //DeadWeight MT
        public double DWT { get; set; }

        //Length between perpendiculars m
        public double LBP { get; set; }

        //Beam m
        public double B { get; set; }

        //Water Depth m
        private double _Wd;
        public double Wd
        {
            get { return _Wd; }
            set
            {
                if (value != _Wd)
                {
                    _Wd = value;
                    NotifyChange("");
                }
            }
        }

        //Draught (average)
        private double _T;
        public double T
        {
            get { return _T; }
            set
            {
                if (value != _T)
                {
                    _T = value;
                    NotifyChange("");
                }
            }
        }

        //Longitudinal (broadside) wind area m^2
        private double _AL;
        public double AL
        {
            get { return _AL; }
            set
            {
                if (value != _AL)
                {
                    _AL = value;
                    NotifyChange("");
                }
            }
        }

        //Above water longitudinal hull area m^2
        private double _AHL;
        public double AHL
        {
            get { return _AHL; }
            set
            {
                if (value != _AHL)
                {
                    _AHL = value;
                    NotifyChange("");
                }
            }
        }

        //Transverse (head-on) wind area m^2
        private double _AT;
        public double AT
        {
            get { return _AT; }
            set
            {
                if (value != _AT)
                {
                    _AT = value;
                    NotifyChange("");
                }
            }
        }

        //Above water transverse hull area m^2
        private double _AHT;
        public double AHT
        {
            get { return _AHT; }
            set
            {
                if (value != _AHT)
                {
                    _AHT = value;
                    NotifyChange("");
                }
            }
        }

        //Height above water/ground surface m
        private double _h;
        public double h
        {
            get { return _h; }
            set
            {
                if (value != _h)
                {
                    _h = value;
                    NotifyChange("");
                }
            }
        }

        //Water depth measured from water surface m
        private double _s;
        public double s
        {
            get { return _s; }
            set
            {
                if (value != _s)
                {
                    _s = value;
                    NotifyChange("");
                }
            }
        }

        //Current velocity (average) m/s
        public double Vc
        {
            get
            {
                if (s > 0)
                    return vc * CoefficientCalculator.GetK(100 * s / T, Wd, T, out List<double> Xs1, out List<double> Ys1);
                return vc;
            }
        }

        //Current velocity at depth s m/s
        private double _vc;
        public double vc
        {
            get { return _vc; }
            set
            {
                if (value != _vc)
                {
                    _vc = value;
                    NotifyChange("");
                }
            }
        }

        //Wind velocity at 10m m/s
        public double Vw
        {
            get
            {
                return vw * Math.Pow(10 / h, 1.0 / 7.0);
            }
        }

        //Wind velocity at elevation h m/s
        private double _vw;
        public double vw
        {
            get { return _vw; }
            set
            {
                if (value != _vw)
                {
                    _vw = value;
                    NotifyChange("");
                }
            }
        }

        //Wind angle of attack deg
        private double _θw;
        public double θw
        {
            get { return _θw; }
            set
            {
                if (value != _θw)
                {
                    _θw = value;
                    NotifyChange("");
                }
            }
        }

        //Current angle of attack deg
        private double _θc;
        public double θc
        {
            get { return _θc; }
            set
            {
                if (value != _θc)
                {
                    _θc = value;
                    NotifyChange("");
                }
            }
        }

        //Coefficients

        //Wind
        public double CXw
        {
            get
            {
                return CoefficientCalculator.GetCoefficient(Coefficient.CXw, θw, vesselType, out List<double> Xs, out List<double> Ys,
                                                            loadingCondition, bowShape, tankShape,
                                                            Wd, T
                                                           );
            }
        }
        public double CYw
        {
            get
            {
                return CoefficientCalculator.GetCoefficient(Coefficient.CYw, θw, vesselType, out List<double> Xs, out List<double> Ys,
                                                            loadingCondition, bowShape, tankShape,
                                                            Wd, T
                                                           );
            }
        }
        public double CXYw
        {
            get
            {
                return CoefficientCalculator.GetCoefficient(Coefficient.CXYw, θw, vesselType, out List<double> Xs, out List<double> Ys,
                                                            loadingCondition, bowShape, tankShape,
                                                            Wd, T
                                                           );
            }
        }

        //Current
        public double CXc
        {
            get
            {
                return CoefficientCalculator.GetCoefficient(Coefficient.CXc, θc, vesselType, out List<double> Xs, out List<double> Ys,
                                                            loadingCondition, bowShape, tankShape,
                                                            Wd, T
                                                           );
            }
        }
        public double CYc
        {
            get
            {
                return CoefficientCalculator.GetCoefficient(Coefficient.CYc, θc, vesselType, out List<double> Xs, out List<double> Ys,
                                                            loadingCondition, bowShape, tankShape,
                                                            Wd, T
                                                           );
            }
        }
        public double CXYc
        {
            get
            {
                return CoefficientCalculator.GetCoefficient(Coefficient.CXYc, θc, vesselType, out List<double> Xs, out List<double> Ys,
                                                            loadingCondition, bowShape, tankShape,
                                                            Wd, T
                                                           );
            }
        }

        //Constructor
        public ForceCalculation()
        {
            this.h = 10;
        }

        //Forces-Moments Calculation
        public double FXw
        {
            get
            {
                return (1.0 / 2.0) * CXw * ρw * Math.Pow(Vw, 2) * AT / 1000;
            }
        }
        public double FYw
        {
            get
            {
                return (1.0 / 2.0) * CYw * ρw * Math.Pow(Vw, 2) * AL / 1000;
            }
        }
        public double MXYw
        {
            get
            {
                return (1.0 / 2.0) * CXYw * ρw * Math.Pow(Vw, 2) * AL * LBP / 1000;
            }
        }

        public double FXc
        {
            get
            {
                return (1.0 / 2.0) * CXc * ρc * Math.Pow(Vc, 2) * LBP * T / 1000;
            }
        }
        public double FYc
        {
            get
            {
                return (1.0 / 2.0) * CYc * ρc * Math.Pow(Vc, 2) * LBP * T / 1000;
            }
        }
        public double MXYc
        {
            get
            {
                return (1.0 / 2.0) * CXYc * ρc * Math.Pow(Vc, 2) * Math.Pow(LBP, 2) * T / 1000;
            }
        }
    }
}