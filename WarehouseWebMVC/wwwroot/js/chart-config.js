const ctx_market = document.getElementById("myChart_Revenue").getContext("2d");

const myChart_Revenue = new Chart(ctx_market, {
  type: "line",

  data: {
    labels: [
      "Jan",
      "Feb",
      "Mar",
      "Apr",
      "May",
      "Jun",
      "Jul",
      "Aug",
      "Sep",
      "Oct",
      "Nov",
      "Dec",
    ],
    datasets: [
      {
        label: "Visitor",
        data: [10, 15, 15, 90, 90, 45, 45, 45, 70, 70, 45, 45],
        backgroundColor: "transparent",
        borderColor: "#F2C94C",
        borderWidth: 5,
        fill: true,
        fillColor: "#fff",
        tension: 0.5,
        pointRadius: 0,
      },
      {
        label: "Sells",
        data: [20, 86, 79, 30, 60, 45, 70, 50, 70, 30, 44, 50],
        backgroundColor: "transparent",
        borderColor: "#09AD95",
        borderWidth: 5,
        fill: true,
        tension: 0.5,
        fillColor: "#fff",
        fill: "start",
        pointRadius: 0,
      },
      {
        label: "Profit",
        data: [20, 20, 79, 80, 60, 45, 70, 30, 20, 90, 44, 50],
        backgroundColor: "transparent",
        borderColor: "#6176FE",
        borderWidth: 5,
        fill: true,
        tension: 0.5,
        fillColor: "#fff",
        fill: "start",
        pointRadius: 0,
      },
    ],
  },

  options: {
    maintainAspectRatio: false,
    responsive: true,
    scales: {
      x: {
        grid: {
          display: true,
          color: "#c5c5c573",
        },
        suggestedMax: 100,
        suggestedMin: 100,
      },
      y: {
        suggestedMax: 100,
        suggestedMin: 100,
        grid: {
          display: true,

          color: "#c5c5c573",
          borderDash: [10, 10],
        },
      },
    },
    plugins: {
      legend: {
        position: "top",
        display: false,
      },
      title: {
        display: false,
        text: "Sell History",
      },
    },
  },
});

setInterval(() => {
  if (document.body.classList.contains("active")) {
    myChart_Revenue.options.scales.y.grid.color = "#E2E7F11C ";
    myChart_Revenue.options.scales.x.grid.color = "#E2E7F11C ";
  } else {
    myChart_Revenue.options.scales.y.grid.color = "#c5c5c573 ";
    myChart_Revenue.options.scales.x.grid.color = "#c5c5c573 ";
  }
  myChart_Revenue.update();
}, 500);

// Chart Seven
const ctx_total_clients = document
  .getElementById("myChart_active_creators")
  .getContext("2d");

const myChart_active_creators = new Chart(ctx_total_clients, {
  type: "line",

  data: {
    labels: ["1", "2", "3", "4", "5"],
    datasets: [
      {
        label: "Total Clients",
        data: [0, 10, 15, 10, 20],
        borderColor: "#F9C200",
        backgroundColor: "transparent",
        pointRadius: 0,
        tension: 0.5,
        borderWidth: 7,
        fill: true,
        fillColor: "#fff",
      },
    ],
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
          display: false,
        },
        suggestedMax: 10,
        suggestedMin: 30,
      },

      y: {
        grid: {
          display: false,
          drawBorder: false,
        },
        ticks: {
          display: false,
        },
        suggestedMax: 10,
        suggestedMin: 30,
      },
    },

    plugins: {
      legend: {
        display: false,
      },
      title: {
        display: false,
      },
    },
  },
});

const ctx_recent_orders = document
  .getElementById("myChart_recent_orders")
  .getContext("2d");

const myChart_recent_orders = new Chart(ctx_recent_orders, {
  type: "line",

  data: {
    labels: ["1", "2", "3", "4", "5"],
    datasets: [
      {
        label: "Total Clients",
        data: [0, 10, 15, 10, 20],
        borderColor: "#6176FE",
        backgroundColor: "transparent",
        pointRadius: 0,
        tension: 0.5,
        borderWidth: 7,
        fill: true,
        fillColor: "#fff",
      },
    ],
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
          display: false,
        },
        suggestedMax: 10,
        suggestedMin: 30,
      },

      y: {
        grid: {
          display: false,
          drawBorder: false,
        },
        ticks: {
          display: false,
        },
        suggestedMax: 10,
        suggestedMin: 30,
      },
    },

    plugins: {
      legend: {
        display: false,
      },
      title: {
        display: false,
      },
    },
  },
});
