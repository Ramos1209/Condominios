function MontarGraficosValoresGanhos(ano) {
    $.ajax({
        url: '/DashBoard/DadosGraficoGanhosAno',
        method: 'GET',
        data: { ano: ano },
        success: function (dados) {
            new Chart(document.getElementById("GraficosValoresGanhosDespesas"),
                {
                    type: 'line',
                    data: {
                        labels: PegarMeses(dados),
                        datasets: [
                            {
                                label: "Ganhos de pagamentos por ano",
                                data: PegarValores(dados),
                                backgroundColor: "#90caf9",
                                borderColor: "#0277db",
                                fill: true,
                                lineTension: 0
                            }
                        ]
                    },
                    options: {
                        legend: {
                            labels: {
                                usePointStyle: true
                            }
                        },

                        scales: {
                            xAxes: [{
                                gridLines: {
                                    display: false
                                }
                            }]
                        }
                    }

                });
        }

    });
}

function MontarGraficosValoresDespesas(ano) {
    $.ajax({
        url: '/DashBoard/DadosGraficoDespesasAno',
        method: 'GET',
        data: { ano: ano },
        success: function (dados) {
            new Chart(document.getElementById("GraficosValoresGanhosDespesas"),
                {
                    type: 'line',
                    data: {
                        labels: PegarMeses(dados),
                        datasets: [
                            {
                                label: "Despesas de serviços por ano",
                                data: PegarValores(dados),
                                backgroundColor: "#ffcdd2",
                                borderColor: "#b71c1c",
                                fill: true,
                                lineTension: 0
                            }
                        ]
                    },
                    options: {
                        legend: {
                            labels: {
                                usePointStyle: true
                            }
                        },

                        scales: {
                            xAxes: [{
                                gridLines: {
                                    display: false
                                }
                            }]
                        }
                    }

                });
        }

    });
}
function MontarGraficoDespesasGanhosTotais() {
    $.ajax({
        url: '/Dashboard/DadosGraficoDespesasGanhosTotais',
        method: 'GET',
        success: function (dados) {
            new Chart(document.getElementById("GraficoDespesasGanhosTotais"), {

                type: 'pie',

                data: {
                    labels: ["Ganhos", "Despesas"],

                    datasets: [{
                        label: "Total de ganhos e despesas",
                        data: [dados.ganhos, dados.despesas],
                        backgroundColor: ["#0091ea", "#c62828"]
                    }]
                },

                options: {
                    legend: {
                        labels: {
                            usePointStyle: true
                        }
                    }
                }
            });
        }
    });
}