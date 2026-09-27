import { useState } from "react";
import axiosClient from "../api/axiosClient";
import Navbar from "../components/Navbar";

const ORDER_NUMBER_PATTERN = /^[A-Za-z0-9]+_\d{8}_\d+$/;

export default function SearchOrderPage() {
  const [orderNumber, setOrderNumber] = useState("");
  const [order, setOrder] = useState(null);
  const [error, setError] = useState("");

  const isValid = ORDER_NUMBER_PATTERN.test(orderNumber);

  const handleSearch = async () => {
    setError("");
    setOrder(null);
    try {
      const res = await axiosClient.get(`/orders/${orderNumber}`);
      setOrder(res.data);
    } catch (err) {
      setError("Order not found.");
    }
  };

  return (
    <div className="min-h-screen bg-gray-100">
      <Navbar />
      <div className="flex justify-center pt-10">
        <div className="bg-white p-8 rounded-xl shadow-md w-full max-w-md">
          <h1 className="text-xl font-bold mb-6">Search by Order Number</h1>

          <input
            type="text"
            placeholder="e.g. client1_26092026_1"
            value={orderNumber}
            onChange={(e) => setOrderNumber(e.target.value)}
            className="w-full border rounded p-2 mb-4"
          />

          <button
            onClick={handleSearch}
            disabled={!isValid}
            className={`w-full py-2 rounded text-white transition ${
              isValid
                ? "bg-blue-600 hover:bg-blue-700"
                : "bg-gray-300 cursor-not-allowed"
            }`}
          >
            Search
          </button>

          {error && (
            <p className="bg-red-100 text-red-700 text-sm p-2 rounded mt-4">
              {error}
            </p>
          )}

          {order && (
            <div className="mt-6 border-t pt-4 text-sm space-y-1">
              <p>
                <span className="font-medium">Order Number:</span>{" "}
                {order.orderNumber}
              </p>
              <p>
                <span className="font-medium">Order Type:</span>{" "}
                {order.orderType}
              </p>
              <p>
                <span className="font-medium">Service:</span>{" "}
                {order.service?.name}
              </p>
              <p>
                <span className="font-medium">Quantity:</span> {order.quantity}
              </p>
              <p>
                <span className="font-medium">Delivery Date:</span>{" "}
                {order.requestedDeliveryDate ?? "-"}
              </p>
              <p>
                <span className="font-medium">Created At:</span>{" "}
                {order.createdAt}
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
