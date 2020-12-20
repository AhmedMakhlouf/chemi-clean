import React, { Component } from "react";
// import { Redirect, Link } from "react-router";

import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";

import * as axios from "axios";
import Pagination from "react-bootstrap/Pagination";
import Button from "react-bootstrap/Button";
import { parseConfigFileTextToJson } from "typescript";

const axiosInstance = axios.create({
  baseURL: "https://localhost:44384/api/TblProducts",
});

export class Products extends Component {
  constructor(props) {
    super(props);

    this.state = {
      products: [],
      loading: true,
      page: 1,
    };
  }

  componentDidMount() {
    this.getProducts(1);
  }

  updatedDaysAgo = (dateStr) => {
    let updated = new Date(JSON.parse(dateStr));
    let now = Date.now();

    const millisecondsPerDay = 24 * 60 * 60 * 1000;
    return parseInt((now - updated) / millisecondsPerDay);
  };

  getProducts = async (page) => {
    try {
      const { data } = await axiosInstance.get(`?page=${page}`);
      const pagesCount = await axios.get(
        "https://localhost:44384/api/TblProducts/PagesCount"
      );
      this.setState({
        products: data,
        loading: false,
        page: page,
        pagesCount: pagesCount.data,
      });

      console.log(pagesCount.data);
    } catch (err) {
      console.log(err.message);
    }
  };

  renderAllProducts(products) {
    return (
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Name</th>
            <th>Supplier Name</th>
            <th>Lastest Update</th>
            <th>Read</th>
          </tr>
        </thead>
        <tbody>
          {this.state.products.map((product) => {
            return (
              <tr key={product}>
                <td>{product.productName}</td>
                <td>{product.supplierName}</td>
                {product.updated != null ? (
                  <td>{this.updatedDaysAgo(Date.now().toString())} Days Ago</td>
                ) : (
                  <td>Never</td>
                )}
                {/* {true && (
                  <td>{this.updatedDaysAgo(Date.now().toString())} Days Ago</td>
                )} */}
                <Button
                  variant="dark"
                  disabled={product.updated == null}
                  onClick={() =>
                    window.open(process.env.PUBLIC_URL + product.uri)
                  }
                >
                  Read
                </Button>
              </tr>
            );
          })}
        </tbody>
      </table>
    );
  }

  render() {
    let content = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderAllProducts(this.state.products)
    );

    return (
      <div>
        <h1>Products</h1>
        <p>Here you can see all products</p>
        {content}
        <div
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <Pagination>
            <Pagination.First onClick={() => this.getProducts(1)} />
            {this.state.page > 1 ? (
              <Pagination.Prev
                onClick={() => this.getProducts(this.state.page - 1)}
              />
            ) : (
              <Pagination.Prev disabled />
            )}

            {this.state.page > 1 && (
              <Pagination.Item
                onClick={() => this.getProducts(this.state.page - 1)}
              >
                {this.state.page - 1}
              </Pagination.Item>
            )}

            <Pagination.Item active>{this.state.page}</Pagination.Item>
            {this.state.page < 10 && (
              <Pagination.Item
                onClick={() => this.getProducts(this.state.page + 1)}
              >
                {this.state.page + 1}
              </Pagination.Item>
            )}

            {this.state.page < this.state.pagesCount ? (
              <Pagination.Next
                onClick={() => this.getProducts(this.state.page + 1)}
              />
            ) : (
              <Pagination.Next disabled />
            )}
            <Pagination.Last
              onClick={() => this.getProducts(this.state.pagesCount)}
            />
          </Pagination>
        </div>
      </div>
    );
  }
}
