import { useState } from "react";
import axiosClient from "../api/axiosClient";
import Navbar from "../components/Navbar";

const ORDER_TYPES = ["Top", "Sun", "Fox"];
const SERVICES = [
  { id: 1, name: "AM" },
  { id: 2, name: "LL" },
  { id: 3, name: "FB" },
  { id: 4, name: "Old LL" },
];

export default function NewOrderPage() {
  const [orderType, setOrderType] = useState("");
  const [serviceId, setServiceId] = useState("");
  const [quantity, setQuantity] = useState("");
  const [deliveryDate, setDeliveryDate] = useState("");
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");

  const validate = () => {
    if (!orderType) return "Order type is required.";
    if (!serviceId) return "Service is required.";
    const q = Number(quantity);
    if (!q || q < 1 || q > 5) return "Quantity must be between 1 and 5.";
    if (deliveryDate) {
      const today = new Date();
      today.setHours(0, 0, 0, 0);
      const selected = new Date(deliveryDate);
      if (selected <= today) return "Delivery date must be after today.";
    }
    return "";
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    const validationError = validate();
    if (validationError) {
      setError(validationError);
      return;
    }
    setError("");

    try {
      const res = await axiosClient.post("/orders", {
        orderType,
        serviceId: Number(serviceId),
        quantity: Number(quantity),
        requestedDeliveryDate: deliveryDate || null,
      });
      setMessage(res.data.message);
      setOrderType("");
      setServiceId("");
      setQuantity("");
      setDeliveryDate("");
    } catch (err) {
      setError(err.response?.data?.title || "Failed to save order.");
    }
  };

  return (
    <div className="min-h-screen bg-gray-100">
      <Navbar />
      <div className="flex justify-center pt-10">
        <form
          onSubmit={handleSubmit}
          className="bg-white p-8 rounded-xl shadow-md w-full max-w-md"
        >
          <h1 className="text-xl font-bold mb-6">New Order</h1>

          {message && (
            <p className="bg-green-100 text-green-700 text-sm p-2 rounded mb-4">
              {message}
            </p>
          )}
          {error && (
            <p className="bg-red-100 text-red-700 text-sm p-2 rounded mb-4">
              {error}
            </p>
          )}

          <label className="block mb-2 text-sm font-medium">Order Type</label>
          <select
            value={orderType}
            onChange={(e) => setOrderType(e.target.value)}
            className="w-full border rounded p-2 mb-4"
          >
            <option value="">-- Select --</option>
            {ORDER_TYPES.map((t) => (
              <option key={t} value={t}>
                {t}
              </option>
            ))}
          </select>

          <label className="block mb-2 text-sm font-medium">Service</label>
          <select
            value={serviceId}
            onChange={(e) => setServiceId(e.target.value)}
            className="w-full border rounded p-2 mb-4"
          >
            <option value="">-- Select --</option>
            {SERVICES.map((s) => (
              <option key={s.id} value={s.id}>
                {s.name}
              </option>
            ))}
          </select>

          <label className="block mb-2 text-sm font-medium">
            Quantity (1-5)
          </label>
          <input
            type="number"
            min="1"
            max="5"
            value={quantity}
            onChange={(e) => setQuantity(e.target.value)}
            className="w-full border rounded p-2 mb-4"
          />

          <label className="block mb-2 text-sm font-medium">
            Requested Delivery Date (optional)
          </label>
          <input
            type="date"
            value={deliveryDate}
            onChange={(e) => setDeliveryDate(e.target.value)}
            className="w-full border rounded p-2 mb-6"
          />

          <button
            type="submit"
            className="w-full bg-blue-600 text-white py-2 rounded hover:bg-blue-700 transition"
          >
            Save Order
          </button>
        </form>
      </div>
    </div>
  );
}
