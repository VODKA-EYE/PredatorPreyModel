using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace PredatorPrey
{
  public partial class MainWindow : Window
  {
    double x0, y0, alpha, beta, delta, gamma;
    public MainWindow()
    {
      InitializeComponent();
      X0.AddHandler(KeyUpEvent, GetValues, RoutingStrategies.Tunnel);
      Y0.AddHandler(KeyUpEvent, GetValues, RoutingStrategies.Tunnel);
      Alpha.AddHandler(KeyUpEvent, GetValues, RoutingStrategies.Tunnel);
      Beta.AddHandler(KeyUpEvent, GetValues, RoutingStrategies.Tunnel);
      Delta.AddHandler(KeyUpEvent, GetValues, RoutingStrategies.Tunnel);
      Gamma.AddHandler(KeyUpEvent, GetValues, RoutingStrategies.Tunnel);
    }

    public void UpdatePlot()
    {
      Plot.Plot.Clear();
      double[] prey = new double[151];
      double[] predators = new double[151];
      prey[0] = x0;
      predators[0] = y0;
      if (alpha != null && beta != null && delta != null && gamma != null)
      {
        for (int i = 0; i < 150; i++)
        {
          prey[i + 1] = (gamma - alpha * predators[i]) * prey[i] + prey[i];
          predators[i + 1] = (delta * prey[i + 1] - beta) * predators[i] + predators[i];
        }
        Plot.Plot.Add.Scatter(prey, predators);
        Plot.Refresh();
      }
    }
    private async void GetValues(object? sender, KeyEventArgs e)
    {
      string whereErrors = null;
      try
      { x0 = Convert.ToDouble(X0.Text.Replace('.', ',')); }
      catch 
      { whereErrors += "X0, "; }
      try
      { y0 = Convert.ToDouble(Y0.Text.Replace('.', ',')); }
      catch
      { whereErrors += "X0, "; }
      try
      { alpha = Convert.ToDouble(Alpha.Text.Replace('.', ',')); }
      catch
      { whereErrors += "α, "; }
      try
      { beta = Convert.ToDouble(Beta.Text.Replace('.', ',')); }
      catch
      { whereErrors += "β, "; }
      try
      { delta = Convert.ToDouble(Delta.Text.Replace('.', ',')); }
      catch
      { whereErrors += "δ, "; }
      try
      { gamma = Convert.ToDouble(Gamma.Text.Replace('.', ',')); }
      catch
      { whereErrors += "γ, "; }
      if (whereErrors != null)
      {
        ErrorMessage.Text = "Ошибочные значения в: " + whereErrors;
      }
      else
      {
        ErrorMessage.Text = "";
        UpdatePlot();
      }
    }
  }
}