terraform {
  required_version = ">= 1.2.5"

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
    "contact" = "Swamy PKV"
  }
}

resource "aws_s3_bucket_acl" "eshop_bucket_acl" {
  bucket = aws_s3_bucket.eshop.id
  acl    = "public-read"
}

resource "aws_s3_object" "eshop_object" {
  key    = "product-1.png"
  bucket = aws_s3_bucket.eshop.id
  source = "content/product-1.png"

  force_destroy = true
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
