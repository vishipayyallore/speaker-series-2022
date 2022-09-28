locals {
  employee_data = file("./data.json")
  employee_rows = jsondecode(local.employee_data)
}
