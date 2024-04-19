# Feature Voting System

### Brief Description

The goal of this project is to create an application that allows product creators to register their products. Users can request new features for these products and vote on the proposed features.

## Table of Contents

- [Main Modules](#main-modules)
- [Users](#users)
- [Product Management](#product-management)
- [Feature Requests](#feature-requests)
- [Voting](#voting)
- [Comments](#comments)
- [Reports](#reports)
- [Notifications](#notifications)

## Main Modules

1. Users
2. Product Management
3. Feature Requests
4. Voting
5. Comments
6. Reports
7. Notifications

## Users

Users should be able to register, authenticate, manage products, and vote on feature requests.

A user should have the following fields:

- First Name *
- Last Name *
- Email *
- Password *
- Registration Date *

## Product Management

Users should be able to register, edit, and delete products.

A product should have the following fields:

- Name *
- Short Description *
- Product Author ID *
- Creation Date *
- Requested Features *
  - Feature Name *
  - Description *
  - Feature Request Author ID *
  - Upvotes *
  - Downvotes *
  - Status * (Active, In Progress, Deleted, Rejected, Completed)
    - Active - Assigned when a user submits a feature request
    - In Progress - Assigned by the product author, indicating that the feature implementation has started
    - Deleted - Assigned when the feature author deletes the request
    - Rejected - Assigned when the product author does not intend to implement the feature (in this case, the reason for rejection must be provided)
    - Completed - Assigned by the product author when the feature implementation is finished
  - Rejection Reason
  - Comments

## Feature Requests

Users should be able to submit feature requests for any product. Within a day, a user should be able to submit a maximum of 10 feature requests per product.

After creating a feature request, the user should be able to edit or delete it.

## Voting

Users should be able to vote (upvote or downvote) on any requested feature for any product. Within a week, a user should be able to vote (upvote or downvote) a maximum of 3 times per product.

## Comments

Users should also be able to leave comments on any requested feature.

## Reports

Users should be able to view the following types of reports:

- **Product Authors**
  - Number of feature requests for the selected period (week/month/all)
  - Total number of votes (upvotes and downvotes separately) on requested features for the selected period (week/month/all)
  - List of features with their statuses and vote counts for the selected period (week/month/all)
- **Users**
  - List of feature requests submitted by the user with their statuses for the selected period (week/month/all)
  - Total number of votes (upvotes and downvotes separately) on requested features for the selected period (week/month/all)

## Notifications

Users should receive the following types of notifications via email:

- When another user:
  - Submits a feature request for a product registered by the user
  - Leaves a comment on an existing feature of a product registered by the user
  - Votes on a feature of a product registered by the user
  - Changes the description of a feature request submitted by the user
- When another user:
  - Votes on a feature request submitted by the user
  - Leaves a comment on a feature request submitted by the user
  - The product author changes the status of a feature request submitted by the user

This README file provides an overview of the "Feature Voting System" project, including its main modules, user roles, and functionalities. It outlines the required fields for users, products, and feature requests, as well as the voting, commenting, reporting, and notification systems. This information should help users understand the project's structure and requirements. Also I provided a picture of an ER diagram of the database (FeatureVotingSystem-ER-Diagram.png) to better understand how entities are relating to each other.
