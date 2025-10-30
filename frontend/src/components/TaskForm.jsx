import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

export default function TaskForm({
	title,
	description,
	onTitleChange,
	onDescriptionChange,
	onSubmit,
}) {
	const handleSubmit = async (e) => {
		e.preventDefault();

		try {
			await axios.post(
				`${API_URL}/tasks?title=${encodeURIComponent(
					title,
				)}&description=${encodeURIComponent(description)}`,
			);
			onSubmit();
		} catch (error) {
			console.error(error);
		}
	};

	return (
		<form onSubmit={handleSubmit}>
			<div className="mb-3">
				<input
					type="text"
					className="form-control"
					placeholder="Task Title"
					value={title}
					onChange={(e) => onTitleChange(e.target.value)}
					required
				/>
			</div>
			<div className="mb-3">
				<textarea
					className="form-control"
					placeholder="Task Description"
					value={description}
					onChange={(e) => onDescriptionChange(e.target.value)}
					rows="3"
				/>
			</div>
			<button type="submit" className="btn btn-primary">
				Add Task
			</button>
		</form>
	);
}
