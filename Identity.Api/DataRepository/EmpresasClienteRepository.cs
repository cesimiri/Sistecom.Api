using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Modelo.Sistecom.Modelo.Database;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

@page : "/empresas"
@inject HttpClient Http
@using Modelo.Sistecom.Modelo.Database

<h3>Gestión de Empresas Clientes</h3>

<!-- Botones principales -->
<div class= "mb-3" >
    < button class= "btn btn-success" @onclick = "NuevoRegistro" > Nuevo </ button >
    < button class= "btn btn-primary" @onclick = "BuscarPorRuc" > Buscar </ button >
    < input class= "form-control d-inline w-25 mx-2" placeholder = "RUC" @bind = "rucBuscar" />
</ div >

< !--Formulario visible solo cuando hay un modelo activo -->
@if (empresaActual != null)
{
    <EditForm Model="@empresaActual" OnValidSubmit="GuardarEmpresa">
        <DataAnnotationsValidator />
        <ValidationSummary />

        <div class= "row" >
            < div class= "col-md-4 mb-2" >
                < label > RUC </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.Ruc" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Razón Social </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.RazonSocial" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Nombre Comercial </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.NombreComercial" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Dirección </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.Direccion" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Ciudad </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.Ciudad" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Teléfono </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.Telefono" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Email </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.Email" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Contacto Principal </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.ContactoPrincipal" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Teléfono Contacto </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.TelefonoContacto" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Tipo Cliente </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.TipoCliente" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Límite Crédito </ label >
                < InputNumber class= "form-control" @bind - Value = "empresaActual.LimiteCredito" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Días Crédito </ label >
                < InputNumber class= "form-control" @bind - Value = "empresaActual.DiasCredito" />
            </ div >

            < div class= "col-md-4 mb-2" >
                < label > Estado </ label >
                < InputText class= "form-control" @bind - Value = "empresaActual.Estado" />
            </ div >
        </ div >

        < button type = "submit" class= "btn btn-primary mt-3" > Guardar </ button >
    </ EditForm >
}

@code {
    private EmpresasCliente empresaActual;
private string rucBuscar;

private async Task NuevoRegistro()
{
    empresaActual = new EmpresasCliente(); // nuevo registro vacío
}

private async Task BuscarPorRuc()
{
    if (!string.IsNullOrWhiteSpace(rucBuscar))
    {
        try
        {
            var response = await Http.GetFromJsonAsync<EmpresasCliente>($"https://localhost:7001/api/EmpresaCliente/GetEmpresaClienteById/{rucBuscar}");
            empresaActual = response;
        }
        catch
        {
            empresaActual = null;
            await JS.InvokeVoidAsync("alert", "Empresa no encontrada.");
        }
    }
}

private async Task GuardarEmpresa()
{
    if (empresaActual.IdempresaCliente == 0)
    {
        // Crear nuevo
        await Http.PostAsJsonAsync("https://localhost:7001/api/EmpresaCliente/InsertEmpresaCliente", empresaActual);
    }
    else
    {
        // Actualizar existente
        await Http.PutAsJsonAsync("https://localhost:7001/api/EmpresaCliente/UpdateEmpresaCliente", empresaActual);
    }

    await JS.InvokeVoidAsync("alert", "Guardado con éxito");
    empresaActual = null;
}

[Inject]
public IJSRuntime JS { get; set; }
}