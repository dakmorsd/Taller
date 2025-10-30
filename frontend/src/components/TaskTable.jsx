export default function TaskTable({ tasks }) {
	if (tasks.length === 0) {
		return <p>No records</p>;
	}

	return (
		<div>
			<h2>Tasks</h2>
			<table className="table table-striped">
				<thead>
					<tr>
						<th>Title</th>
						<th>Description</th>
					</tr>
				</thead>
				<tbody>
					{tasks.map((task) => (
						<tr key={task.id}>
							<td>{task.title}</td>
							<td>{task.description}</td>
						</tr>
					))}
				</tbody>
			</table>
		</div>
	);
}
