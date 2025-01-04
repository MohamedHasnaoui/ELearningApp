window.initChart = () => {
    var options = {
        chart: {
            type: 'line'
        },
        series: [{
            name: 'Example',
            data: [10, 20, 30]
        }]
    };

    var chart = new ApexCharts(document.querySelector("#chart"), options);
    chart.render();
};
