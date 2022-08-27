terraform {
  required_version = ">= 1.2.6"

  cloud {
    organization = "swamy-the-tf-learner"

    workspaces {
      name = "First-Simple-Work-Space"
    }
  }

  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "4.24.0"
    }
  }
}

provider "aws" {
  # Configuration options
  region = var.region
}

resource "aws_s3_bucket" "eshop" {
  bucket = "${var.eshop_bucket_subdomain}.${var.root_domain}"

  tags = {
    "name"    = "s3_bucket_eshop"
    "purpose" = "eShop Static Website"
    "contact" = "Swamy PKV"
  }

}

resource "aws_s3_bucket_website_configuration" "eshop" {
  bucket = aws_s3_bucket.eshop.id

  index_document {
    suffix = "index.html"
  }

  error_document {
    key = "error.html"
  }
}

resource "aws_s3_bucket_acl" "eshop_bucket_acl" {
  bucket = aws_s3_bucket.eshop.id
  acl    = "public-read"
}

resource "aws_s3_object" "upload_object" {
  for_each     = fileset(var.upload_directory, "**/*.*")
  bucket       = aws_s3_bucket.eshop.id
  key          = replace(each.value, var.upload_directory, "")
  source       = "${var.upload_directory}${each.value}"
  etag         = filemd5("${var.upload_directory}${each.value}")
  content_type = lookup(var.mime_types, split(".", each.value)[length(split(".", each.value)) - 1])
}

resource "aws_s3_bucket_policy" "read_access_policy" {
  bucket = aws_s3_bucket.eshop.id
  policy = <<POLICY
          {
              "Version": "2012-10-17",
              "Statement": [
                  {
                      "Sid": "PublicReadGetObject",
                      "Effect": "Allow",
                      "Principal": "*",
                      "Action": [
                          "s3:GetObject"
                      ],
                      "Resource": [
                          "arn:aws:s3:::${aws_s3_bucket.eshop.id}/*"
                      ]
                  }
              ]
          }
POLICY
}

# resource "aws_s3_object" "upload_object" {
#   for_each     = fileset("content/", "*")
#   bucket       = aws_s3_bucket.eshop.id
#   key          = each.value
#   source       = "content/${each.value}"
#   etag         = filemd5("content/${each.value}")
#   content_type = "text/html"
# }


# resource "aws_s3_object" "upload_cssfiles" {
#   for_each     = fileset("content/styles/", "*")
#   bucket       = aws_s3_bucket.eshop.id
#   key          = each.value
#   source       = "content/styles/${each.value}"
#   etag         = filemd5("content/styles/${each.value}")
#   content_type = "text/css"
# }

# resource "aws_s3_object" "upload_jsfiles" {
#   for_each     = fileset("content/scripts/", "*")
#   bucket       = aws_s3_bucket.eshop.id
#   key          = each.value
#   source       = "content/scripts/${each.value}"
#   etag         = filemd5("content/scripts/${each.value}")
#   content_type = "application/javascript"
# }
