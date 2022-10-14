variable "region" {
  default = "us-west-2" # Oregon
}

variable "root_domain" {
  default = "s3-bucket-dev-002"
}

variable "eshop_bucket_subdomain" {
  default = "eshop"
}

variable "mime_types" {
  default = {
    ico  = "image/x-icon"
    html = "text/html"
    css  = "text/css"
    js   = "application/javascript"
  }
}

variable "upload_directory" {
  default = "content/"
}
