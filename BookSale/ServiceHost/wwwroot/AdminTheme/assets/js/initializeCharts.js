const barChartDiv = document.getElementById("bar");
const barChart = new Chart(barChartDiv,
    {
        type: "bar",
        data: {
            labels: ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"],
            datasets: [
                {
                    label: ["Apple"],
                    data: [100, 110, 120, 160, 210, 175, -10, 25, 98, 200, 120,170],
                    backgroundColor: "#ffcdb2",
                    borderColor: "#b5838d"
                },
                {
                    label: ["Samsung"],
                    data: [150, 160, 180, 210, 290, 240, -20, 75, 110, 220, 170,225],
                    backgroundColor: "#ffcdb2",
                    borderColor: "#b5838d"
                }

            ]
        },
        options: {
            elements: {
                bar: {
                    borderWidth: 1
                }
            }
        }
    });


const polarAreaChartDiv = document.getElementById("polarArea");
const polarAreaChart = new Chart(polarAreaChartDiv,
    {
        type: "polarArea",
        data: {
            labels: ["سیب", "هندونه", "گلابی", "پرتغال", "انار"],
            datasets: [
                {
                    label: ["fruit"],
                    data: [100, 110, 120, 160, 210],
                    backgroundColor: ["#E76F51", "#F4A261", "#E9C46A", "#2A9D8F", "#264653"],
                    borderColor: "#b5838d"
                }
            ]
        },
        options: {
            elements: {
                polarArea: {
                    borderWidth: 1
                }
            }
        }
    });