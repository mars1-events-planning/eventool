<script lang="ts">
	let { value, ...props } = $props<{
		id: string;
		name: string;
		value: string;
		label: string | undefined;
		labelClass: string | '';
		inputClass: string | '';
	}>();
</script>


{#if props.label}
<label for={props.id} class={`label ${props.labelClass}`}>
    {props.label}
</label>
{/if}
<textarea
	id={props.id}
	name={props.name}
	bind:value={value}
	class={`input ${props.inputClass}`}
/>
