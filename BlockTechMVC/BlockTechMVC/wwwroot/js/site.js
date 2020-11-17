// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function TrocarGrafico() {
    document.getElementById('graficoSemana').style.display = 'none'
    document.getElementById('graficoMes').style.display = 'block'
    document.getElementById('botaoMes').style.display = 'none'
    document.getElementById('botaoSemana').style.display = 'block'
}

function TrocarGraficoSemana() {
    document.getElementById('graficoSemana').style.display = 'block'
    document.getElementById('graficoMes').style.display = 'none'
    document.getElementById('botaoMes').style.display = 'block'
    document.getElementById('botaoSemana').style.display = 'none'
}
