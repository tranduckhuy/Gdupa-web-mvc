// Chart One
const ctx = document.getElementById('myChart_one_monthly').getContext('2d');
const myChart_one_monthly = new Chart(ctx, {
	type: 'bar',

	data: {
		labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16'],
		datasets: [{
			label: 'Profit',
			data: [90, 60, 85, 40, 75, 45, 95, 75, 45, 65, 35, 90, 40, 50, 30, 70],
			backgroundColor: [
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
				'#F9C200',
			],
			fill: true,
			tension: 0.4,
			borderWidth: 0,
			borderSkipped: false,
			borderRadius: 4,
			barPercentage: 0.7,
			categoryPercentage: 0.5,
		}, {
			label: 'Profit',
			data: [85, 55, 80, 45, 70, 50, 90, 60, 55, 60, 45, 85, 50, 60, 40, 65],
			backgroundColor: [
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
				'#6176FE',
			],
			fill: true,
			tension: 0.4,
			borderWidth: 0,
			borderSkipped: false,
			borderRadius: 4,
			barPercentage: 0.7,
			categoryPercentage: 0.5,
		}, {
			label: 'Profit',
			data: [90, 60, 85, 40, 75, 45, 95, 75, 45, 65, 35, 90, 40, 50, 30, 90],
			backgroundColor: [
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
				'#09AD95',
			],
			fill: true,
			tension: 0.4,
			borderWidth: 0,
			borderSkipped: false,
			borderRadius: 4,
			barPercentage: 0.7,
			categoryPercentage: 0.5,
		},
		]
	},
	options: {
		intersect: false,
		maintainAspectRatio: false,
		responsive: true,
		scales: {
			x: {
				ticks: {
				},
				grid: {
					display: false,
				},

			},
			y: {
				ticks: {
					callback: function (value, index, values) {
						return value + '%';
					}
				},
				grid: {
					drawBorder: false,
					color: '#c5c5c573',
					borderDash: [10, 10]
				},
			},


		},
		plugins: {
			tooltip: {
				padding: 10,
				displayColors: true,
				yAlign: 'bottom',
				backgroundColor: '#fff',
				titleColor: '#000',
				titleFont: { weight: 'normal' },
				bodyColor: '#2F3032',
				cornerRadius: 12,
				font: {
					size: 14
				},
				caretSize: 9,
				bodySpacing: 100,

			},
			legend: {
				position: 'top',
				display: false,
			},
			title: {
				display: false,
				text: 'Sell History'
			}
		}
	}
});
setInterval(() => {
	if (document.body.classList.contains('active')) {
		myChart_one_monthly.options.scales.y.grid.color = '#E2E7F11C ';
	} else {
		myChart_one_monthly.options.scales.y.grid.color = '#c5c5c573 ';
	}
	myChart_one_monthly.update();
}, 500);


// Chart Two
const ctx_two = document.getElementById('myChart_Total_Sales_Home').getContext('2d');

const gradientBg = ctx_two.createLinearGradient(0, 0, 0, 190);

gradientBg.addColorStop(0, 'rgba(97, 118, 254, 0.43)');
gradientBg.addColorStop(1, 'rgba(97, 118, 254, 0)');
const myChart_Total_Sales_Home = new Chart(ctx_two, {
	type: 'line',

	data: {
		labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
		datasets: [{
			label: 'Visitor',
			data: [20, 15, 35, 45, 60, 45, 70, 50, 70, 70, 44, 50],
			backgroundColor: gradientBg,
			borderColor: '#6176FE',
			pointRadius: 0,
			tension: 0.5,
			borderWidth: 6,
			fill: true,
			fillColor: '#fff',
		}]
	},

	options: {

		maintainAspectRatio: false,
		responsive: true,
		scales: {
			x: {
				grid: {
					display: true,
					color: '#c5c5c573',
				},
				suggestedMax: 80, suggestedMin: 80,

			},
			y: {
				suggestedMax: 80, suggestedMin: 80,
				grid: {
					display: true,

					color: '#c5c5c573',
					borderDash: [10, 10]
				},
			},
		},

		plugins: {
			legend: {
				position: 'bottom',
				display: false,
			},
			title: {
				display: false,
			}
		}
	}
});

setInterval(() => {
	if (document.body.classList.contains('active')) {
		myChart_Total_Sales_Home.options.scales.x.grid.color = '#E2E7F11C ';
		myChart_Total_Sales_Home.options.scales.y.grid.color = '#E2E7F11C ';
	} else {
		myChart_Total_Sales_Home.options.scales.x.grid.color = '#c5c5c573 ';
		myChart_Total_Sales_Home.options.scales.y.grid.color = '#c5c5c573 ';
	}
	myChart_Total_Sales_Home.update();
}, 500);

// Chart Revenue
const ctx_market = document.getElementById('myChart_Revenue').getContext('2d');

const myChart_Revenue = new Chart(ctx_market, {
	type: 'line',

	data: {
		labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
		datasets: [{
			label: 'Visitor',
			data: [10, 15, 15, 90, 90, 45, 45, 45, 70, 70, 45, 45],
			backgroundColor: 'transparent',
			borderColor: '#F2C94C',
			borderWidth: 5,
			fill: true,
			fillColor: '#fff',
			tension: 0.5,
			pointRadius: 0,
		},
		{
			label: 'Sells',
			data: [20, 86, 79, 30, 60, 45, 70, 50, 70, 30, 44, 50],
			backgroundColor: 'transparent',
			borderColor: '#09AD95',
			borderWidth: 5,
			fill: true,
			tension: 0.5,
			fillColor: '#fff',
			fill: 'start',
			pointRadius: 0,
		},
		{
			label: 'Profit',
			data: [20, 20, 79, 80, 60, 45, 70, 30, 20, 90, 44, 50],
			backgroundColor: 'transparent',
			borderColor: '#6176FE',
			borderWidth: 5,
			fill: true,
			tension: 0.5,
			fillColor: '#fff',
			fill: 'start',
			pointRadius: 0,
		}]
	},

	options: {
		maintainAspectRatio: false,
		responsive: true,
		scales: {
			x: {
				grid: {
					display: true,
					color: '#c5c5c573',
				},
				suggestedMax: 100, suggestedMin: 100,

			},
			y: {
				suggestedMax: 100, suggestedMin: 100,
				grid: {
					display: true,

					color: '#c5c5c573',
					borderDash: [10, 10]
				},
			},
		},
		plugins: {
			legend: {
				position: 'top',
				display: false,
			},
			title: {
				display: false,
				text: 'Sell History'
			}
		}
	}
});

setInterval(() => {
	if (document.body.classList.contains('active')) {
		myChart_Revenue.options.scales.y.grid.color = '#E2E7F11C ';
		myChart_Revenue.options.scales.x.grid.color = '#E2E7F11C ';
	} else {
		myChart_Revenue.options.scales.y.grid.color = '#c5c5c573 ';
		myChart_Revenue.options.scales.x.grid.color = '#c5c5c573 ';
	}
	myChart_Revenue.update();
}, 500);




// Chart Seven
const ctx_total_clients = document.getElementById('myChart_active_creators').getContext('2d');

const myChart_active_creators = new Chart(ctx_total_clients, {
	type: 'line',

	data: {
		labels: ['1', '2', '3', '4', '5'],
		datasets: [{
			label: 'Total Clients',
			data: [0, 10, 15, 10, 20],
			borderColor: '#F9C200',
			backgroundColor: 'transparent',
			pointRadius: 0,
			tension: 0.5,
			borderWidth: 7,
			fill: true,
			fillColor: '#fff',
		}]
	},

	options: {

		maintainAspectRatio: false,
		responsive: true,
		scales: {
			x: {
				grid: {
					display: false,
					drawBorder: false,
				},
				ticks: {
					display: false
				},
				suggestedMax: 10, suggestedMin: 30,

			},

			y: {
				grid: {
					display: false,
					drawBorder: false,
				},
				ticks: {
					display: false
				},
				suggestedMax: 10, suggestedMin: 30,
			},
		},

		plugins: {
			legend: {
				display: false,
			},
			title: {
				display: false,
			}
		}
	}
});

const ctx_recent_orders = document.getElementById('myChart_recent_orders').getContext('2d');

const myChart_recent_orders = new Chart(ctx_recent_orders, {
	type: 'line',

	data: {
		labels: ['1', '2', '3', '4', '5'],
		datasets: [{
			label: 'Total Clients',
			data: [0, 10, 15, 10, 20],
			borderColor: '#09AD95',
			backgroundColor: 'transparent',
			pointRadius: 0,
			tension: 0.5,
			borderWidth: 7,
			fill: true,
			fillColor: '#fff',
		}]
	},

	options: {

		maintainAspectRatio: false,
		responsive: true,
		scales: {
			x: {
				grid: {
					display: false,
					drawBorder: false,
				},
				ticks: {
					display: false
				},
				suggestedMax: 10, suggestedMin: 30,

			},

			y: {
				grid: {
					display: false,
					drawBorder: false,
				},
				ticks: {
					display: false
				},
				suggestedMax: 10, suggestedMin: 30,
			},
		},

		plugins: {
			legend: {
				display: false,
			},
			title: {
				display: false,
			}
		}
	}
});