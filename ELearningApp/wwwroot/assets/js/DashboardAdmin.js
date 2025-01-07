
        // =========================== Double Line Chart Start ===============================
    function createLineChart(chartId, chartColor) {
                var options = {
        series: [
    {
        name: 'Study',
    data: [8, 15, 9, 20, 10, 33, 13, 22, 8, 17, 10, 15],
                    },
    {
        name: 'Test',
    data: [8, 24, 18, 40, 18, 48, 22, 38, 18, 30, 20, 28],
                    },
    ],
    chart: {
        type: 'area',
    width: '100%',
    height: 300,
    sparkline: {
        enabled: false // Remove whitespace
                    },
    toolbar: {
        show: false
                    },
    padding: {
        left: 0,
    right: 0,
    top: 0,
    bottom: 0
                    }
                },
    colors: ['#3D7FF9', chartColor],  // Set the color of the series
    dataLabels: {
        enabled: false,
                },
    stroke: {
        curve: 'smooth',
    width: 1,
    colors: ["#3D7FF9", chartColor],
    lineCap: 'round',
                },
    fill: {
        type: 'gradient',
    gradient: {
        shadeIntensity: 1,
    opacityFrom: 0.9,  // Decrease this value to reduce opacity
    opacityTo: 0.2,    // Decrease this value to reduce opacity
    stops: [0, 100]
                    }
                },
    grid: {
        show: true,
    borderColor: '#E6E6E6',
    strokeDashArray: 3,
    position: 'back',
    xaxis: {
        lines: {
        show: false
                        }
                    },
    yaxis: {
        lines: {
        show: true
                        }
                    },
    row: {
        colors: undefined,
    opacity: 0.5
                    },
    column: {
        colors: undefined,
    opacity: 0.5
                    },
    padding: {
        top: 0,
    right: 0,
    bottom: 0,
    left: 0
                    },
                },
    // Customize the circle marker color on hover
    markers: {
        colors: ["#3D7FF9", chartColor],
    strokeWidth: 3,
    size: 0,
    hover: {
        size: 8
                    }
                },
    xaxis: {
        labels: {
        show: false
                        },
    categories: [`Jan`, `Feb`, `Mar`, `Apr`, `May`, `Jun`, `Jul`, `Aug`, `Sep`, `Oct`, `Nov`, `Dec`],
    tooltip: {
        enabled: false,
                        },
    labels: {
        formatter: function (value) {
                                return value;
                            },
    style: {
        fontSize: "14px"
                            }
                        },
                    },
    yaxis: {
        labels: {
        formatter: function (value) {
                                    return "$" + value + "Hr";
                                },
    style: {
        fontSize: "14px"
                                }
                            },
                    },
    tooltip: {
        x: {
        format: 'dd/MM/yy HH:mm'
                        },
                    },
    legend: {
        show: false,
    position: 'top',
    horizontalAlign: 'right',
    offsetX: -10,
    offsetY: -0
                    }
                };

    var chart = new ApexCharts(document.querySelector(`#${chartId}`), options);
    chart.render();
            }
    createLineChart('doubleLineChartAdmin', '#27CFA7');
    // =========================== Double Line Chart End ===============================
    // ============================ Donut Chart Start ==========================
    var options = {
        series: [65.2, 25, 9.8],
    chart: {
        height: 200,
    type: 'donut',
                },
    colors: ['#3D7FF9', '#27CFA7', '#FA902F'],
    enabled: true, // Enable data labels
    formatter: function (val, opts) {
                    return opts.w.config.series[opts.seriesIndex] + '%';
                },
    dropShadow: {
        enabled: false
                },
    plotOptions: {
        pie: {
        donut: {
        size: '55%' // Fixed slice width
                        }
                    }
                },
    responsive: [{
        breakpoint: 480,
    options: {
        chart: {
        width: 200
                        },
    legend: {
        show: false
                        }
                    }
                }],
    legend: {
        position: 'right',
    offsetY: 0,
    height: 230,
    show: false
                }
            };

    var chart = new ApexCharts(document.querySelector("#activityDonutChartAdmin"), options);
    chart.render();
    // ============================ Donut Chart End ==========================
    function createChart(chartId, chartColor) {

        let currentYear = new Date().getFullYear();

    var options = {
        series: [
    {
        name: 'series1',
    data: [18, 25, 22, 40, 34, 55, 50, 60, 55, 65],
                    },
    ],
    chart: {
        type: 'area',
    width: 80,
    height: 42,
    sparkline: {
        enabled: true // Remove whitespace
                    },

    toolbar: {
        show: false
                    },
    padding: {
        left: 0,
    right: 0,
    top: 0,
    bottom: 0
                    }
                },
    dataLabels: {
        enabled: false
                },
    stroke: {
        curve: 'smooth',
    width: 1,
    colors: [chartColor],
    lineCap: 'round'
                },
    grid: {
        show: true,
    borderColor: 'transparent',
    strokeDashArray: 0,
    position: 'back',
    xaxis: {
        lines: {
        show: false
                        }
                    },
    yaxis: {
        lines: {
        show: false
                        }
                    },
    row: {
        colors: undefined,
    opacity: 0.5
                    },
    column: {
        colors: undefined,
    opacity: 0.5
                    },
    padding: {
        top: 0,
    right: 0,
    bottom: 0,
    left: 0
                    },
                },
    fill: {
        type: 'gradient',
    colors: [chartColor], // Set the starting color (top color) here
    gradient: {
        shade: 'light', // Gradient shading type
    type: 'vertical',  // Gradient direction (vertical)
    shadeIntensity: 0.5, // Intensity of the gradient shading
    gradientToColors: [`${chartColor}00`], // Bottom gradient color (with transparency)
    inverseColors: false, // Do not invert colors
    opacityFrom: .5, // Starting opacity
    opacityTo: 0.3,  // Ending opacity
    stops: [0, 100],
                    },
                },
    // Customize the circle marker color on hover
    markers: {
        colors: [chartColor],
    strokeWidth: 2,
    size: 0,
    hover: {
        size: 8
                    }
                },
    xaxis: {
        labels: {
        show: false
                    },
    categories: [`Jan ${currentYear}`, `Feb ${currentYear}`, `Mar ${currentYear}`, `Apr ${currentYear}`, `May ${currentYear}`, `Jun ${currentYear}`, `Jul ${currentYear}`, `Aug ${currentYear}`, `Sep ${currentYear}`, `Oct ${currentYear}`, `Nov ${currentYear}`, `Dec ${currentYear}`],
    tooltip: {
        enabled: false,
                    },
                },
    yaxis: {
        labels: {
        show: false
                    }
                },
    tooltip: {
        x: {
        format: 'dd/MM/yy HH:mm'
                    },
                },
                };

    var chart = new ApexCharts(document.querySelector(`#${chartId}`), options);
    chart.render();
            }

    // Call the function for each chart with the desired ID and color
    createChart('complete-courseAdmin', '#2FB2AB');
    createChart('earned-certificateAdmin', '#27CFA7');
    createChart('course-progressAdmin', '#6142FF');
    createChart('community-supportAdmin', '#FA902F');