// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function TrocarGrafico() {
    document.getElementById('graficoBitcoin').style.display = 'none'
    document.getElementById('graficoBitcoin30').style.display = 'block'
    document.getElementById('botaoMes').style.display = 'none'
    document.getElementById('botaoSemana').style.display = 'block'
}

function TrocarGraficoSemana() {
    document.getElementById('graficoBitcoin').style.display = 'block'
    document.getElementById('graficoBitcoin30').style.display = 'none'
    document.getElementById('botaoMes').style.display = 'block'
    document.getElementById('botaoSemana').style.display = 'none'
}