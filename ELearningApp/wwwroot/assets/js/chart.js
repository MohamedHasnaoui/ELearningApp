window.initializeChart = (data) => {
    var ctx = document.getElementById('myChart').getContext('2d');
    
    // Préparer les labels, les datasets, etc.
    var labels = [];
    var datasets = [
        {
            label: 'Nombre d\'étudiants par mois',
            data: [],
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
        }
    ];

    // Remplir les données du graphique
    data.forEach(item => {
        labels.push(item.courseName + " " + item.month + "/" + item.year);
        datasets[0].data.push(item.studentCount);
    });

    // Initialisation du graphique
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: datasets
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}
